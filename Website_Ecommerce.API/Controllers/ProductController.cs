using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.ModelDtos;
using Website_Ecommerce.API.Repositories;
using Website_Ecommerce.API.Response;

namespace Website_Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "MyAuthKey")]
    // [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public ProductController(
            IProductRepository productRepository,
            IShopRepository shopRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IHttpContextAccessor httpContext)
        {
            _productRepository = productRepository;
            _shopRepository = shopRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContext = httpContext;

        }
#region Product
        
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto request, CancellationToken cancellationToken)
        {
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());
            // int shopId = await _shopRepository.Shops.Where(x => x.UserId == userId).Select(x => x.Id);
            Product product = new Product();
            product.Name = request.Name;
            product.Material = request.Material;
            product.ShopId = request.ShopId; //get shopid from token
            product.Origin = request.Origin;
            product.Description = request.Description;
            product.Status = request.Status;
            _productRepository.Add(product);
            var result = await _productRepository.UnitOfWork.SaveAsync(cancellationToken);
            if(result == 0)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.ExcuteDB,
                    Result = new ResponseDefault()
                    {
                        Data = "Add product fail"
                    }
                });
            }

            //add product categories
            ProductCategory pc = new ProductCategory();
            foreach(var items in request.Categories)
            {
                pc.ProductId = product.Id;
                pc.CategoryId = items;
                _productRepository.Add(pc);
            }


            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = "Add Product success"
                }
            });
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto request, CancellationToken cancellationToken)
        {
            string userName = _httpContext.HttpContext.User.Identity.Name.ToString();
            //lay userId, nguoi tao la nguoi xoa
            Product product = _productRepository.Products.FirstOrDefault(p => p.Id == request.Id);
            if(product == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "Not Found Product"
                    }
                });
            }

            product.Name = request.Name;
            product.Material = request.Material;
            product.ShopId = request.ShopId; //get shopid from token
            product.Origin = request.Origin;
            product.Description = request.Description;
            product.Status = request.Status;
            _productRepository.Update(product);
            var result = await _productRepository.UnitOfWork.SaveAsync(cancellationToken);

            List<ProductCategory> cateOld = _productRepository.ProductCategories.Where(p => p.ProductId == product.Id).ToList();
            if (cateOld.Count != 0)
            {
                //get cate giu nguyen
                HashSet<ProductCategory> cateSame = new HashSet<ProductCategory>();
                foreach (int cateId in request.Categories)
                {
                    ProductCategory same = cateOld.FirstOrDefault(x => x.CategoryId == cateId);
                    if (same != null)
                    {
                        cateSame.Add(same);
                    }
                }
                //get cate exam delete 
                List<ProductCategory> cateDel = cateOld.Except(cateSame).ToList();
                foreach (ProductCategory examDel in cateDel)
                {
                    _productRepository.Delete(examDel);
                }
                HashSet<int> cateNew = request.Categories
                    .Except(cateSame.Select(x => x.CategoryId)
                    .ToHashSet()).ToHashSet();
                foreach (int cateIdNew in cateNew)
                {
                    _productRepository.Add(new ProductCategory() { CategoryId = cateIdNew, ProductId = product.Id });
                }
            }
            else
            {
                foreach (int cateId in request.Categories)
                {
                    _productRepository.Add(new ProductCategory() { CategoryId = cateId, ProductId = product.Id });
                }
            }

            if(result > 0)
            {
                return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = product.Id.ToString()
                    }
                });
            }
            return BadRequest( new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Update product fail"
                }
            });
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if(id.ToString() is null)
            {
                return BadRequest(null);
            }
            //get shopId, check shop tao => moi xoa
            Product product = await _productRepository.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(product == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "NotFound Product"
                    }
                });
            }
            product.Status = false;
            _productRepository.Update(product);
            var result = await _productRepository.UnitOfWork.SaveAsync();

            if(result > 0)
            {
                return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = product.Id.ToString()
                    }
                });
            }
            return BadRequest( new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Delete category fail"
                }
            });
        }

        [HttpGet("get-product-by/{id}")]
        public async Task<IActionResult> GetProductById([FromQuery] int id)
        {
            if(id.ToString() is null)
            {
                return BadRequest(null);
            }
            //get shopId, check shop tao => moi xoa
            Product product = await _productRepository.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(product == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "NotFound Product"
                    }
                });
            }

            return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = product
                    }
                });
        }

        [HttpGet("get-list-product")]
        public async Task<IActionResult> GetListProduct()
        {
            
            List<Product> products = await _productRepository.Products.ToListAsync();
            List<ProductDto> productDtos = _mapper.Map<List<ProductDto>>(products);
            if(products == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "NotFound Product"
                    }
                });
            }

            return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = products
                    }
                });
        }


#endregion

#region ProductImage
        
        [HttpPost("add-product-image")]
        public async Task<IActionResult> AddProductImage([FromBody] ProductImageDto request, CancellationToken cancellationToken)
        {
            ProductImage productImage = new ProductImage();
            productImage.UrlImage = request.UrlImage;
            productImage.ProductDetailId = request.ProductDetailId;
            _productRepository.Add(productImage);
            var result = await _productRepository.UnitOfWork.SaveAsync(cancellationToken);
            if(result == 0)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.ExcuteDB,
                    Result = new ResponseDefault()
                    {
                        Data = "Add ProductImage fail"
                    }
                });
            }
            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = "Add ProductImage success"
                }
            });
        }

        [HttpDelete("delete-product-image-by/{id}")]
        public async Task<IActionResult> DeleteProductImage([FromQuery] int id)
        {
            if(id.ToString() is null)
            {
                return BadRequest(null);
            }
            //get shopId, check shop tao => moi xoa
            ProductImage product = await _productRepository.ProductImages.FirstOrDefaultAsync(p => p.Id == id);
            if(product == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "NotFound ProductImage"
                    }
                });
            }

            _productRepository.Delete(product);
            var result = await _productRepository.UnitOfWork.SaveAsync();

            if(result > 0)
            {
                return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = product.Id.ToString()
                    }
                });
            }
            return BadRequest( new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Delete ProductImage fail"
                }
            });
        }

#endregion

        
#region ProductDetail

        [HttpPost("add-product-detail")]
        public async Task<IActionResult> AddProductDetail([FromBody] ProductDetailDto request, CancellationToken cancellationToken)
        {
            var productImage = _mapper.Map<ProductImage>(request);

            _productRepository.Add(productImage);
            var result = await _productRepository.UnitOfWork.SaveAsync(cancellationToken);
            if(result == 0)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.ExcuteDB,
                    Result = new ResponseDefault()
                    {
                        Data = "Add ProductDetail fail"
                    }
                });
            }
            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = "Add ProductDetail success"
                }
            });
        }


        [HttpPut("update-product-detail")]
        public async Task<IActionResult> UpdateProductDetail([FromBody] ProductDetailDto request, CancellationToken cancellationToken)
        {
            ProductDetail product = _productRepository.ProductDetails.FirstOrDefault(p => p.Id == request.Id);
            if(product == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "Not Found ProductDetail"
                    }
                });
            }
            product = _mapper.Map<ProductDetail>(request);

            _productRepository.Update(product);
            var result = await _productRepository.UnitOfWork.SaveAsync(cancellationToken);

            if(result > 0)
            {
                return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = product.Id.ToString()
                    }
                });
            }
            return BadRequest( new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Update ProductDetail fail"
                }
            });
        }



        [HttpDelete("delete-product-detail-by/{id}")]
        public async Task<IActionResult> DeleteProductDetail([FromQuery] int id)
        {
            if(id.ToString() is null)
            {
                return BadRequest(null);
            }

            ProductDetail product = await _productRepository.ProductDetails.FirstOrDefaultAsync(p => p.Id == id);
            if(product == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "NotFound ProductImage"
                    }
                });
            }

            _productRepository.Delete(product);
            var result = await _productRepository.UnitOfWork.SaveAsync();

            if(result > 0)
            {
                return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = product.Id.ToString()
                    }
                });
            }
            return BadRequest( new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Delete ProductDetail fail"
                }
            });
        }

        [HttpGet("get-product-detail-by/{productId}")]
        public async Task<IActionResult> GetProductDetailByProductId(int productId)
        {
            List<ProductDetail> productDetails = await _productRepository.ProductDetails.Where(x => x.ProductId == productId).ToListAsync();

            return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = productDetails
                    }
                });
        }

#endregion



    }
}