using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContext;

        public ProductController(
            IProductRepository productRepository,
            IHttpContextAccessor httpContext)
        {
            _productRepository = productRepository;
            _httpContext = httpContext;

        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto request, CancellationToken cancellationToken)
        {
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

        [HttpPost("update-product")]
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
        public async Task<IActionResult> DeleteProduct([FromQuery] int id)
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
                    Data = "Delete category fail"
                }
            });
        }


        // [HttpGet("get-list-product-by-shopId/{id}")]e
        // public async Task<IActionResult> GetListProduct(int shopId)
        // {
            
        //     string userName = _httpContext.HttpContext.User.Identity.Name.ToString();
        //     return Ok(userName);
        // }

        [HttpGet("get-username-from-token")]
        public async Task<IActionResult> GetUSername()
        {
            string userName =  _httpContext.HttpContext.User.Identity.Name.ToString();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

            }

            return Ok(userName);
            // return Ok(identity.Claims);

        }
    }
}