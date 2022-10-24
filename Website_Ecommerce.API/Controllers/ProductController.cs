using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
            product.CategoryId = product.CategoryId;
            product.Status = request.Status;
            _productRepository.Add(product);
            var result = await _productRepository.UnitOfWork.SaveAsync(cancellationToken);
            if(result > 0)
            {
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
            return BadRequest( new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Excute Db error"
                }
            });
        }

        [HttpPost("update-product")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto request, CancellationToken cancellationToken)
        {
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
                        Data = "NotFound Product"
                    }
                });
            }

            product.Name = request.Name;
            product.Material = request.Material;
            product.ShopId = request.ShopId; //get shopid from token
            product.Origin = request.Origin;
            product.Description = request.Description;
            product.CategoryId = product.CategoryId;
            product.Status = request.Status;
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
    }
}