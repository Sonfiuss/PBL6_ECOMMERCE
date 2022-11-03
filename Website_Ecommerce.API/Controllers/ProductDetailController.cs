using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Semester7.PBL6.Website_Ecommerce.API.ModelDtos.CartDto;
using Semester7.PBL6.Website_Ecommerce.API.Repositories;
using Website_Ecommerce.API.Data.Entities;

namespace Semester7.PBL6.Website_Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly IProductDetailRepository _productDetailRepository;

        public ProductDetailController(IProductDetailRepository productDetailRepository){
            _productDetailRepository = productDetailRepository;
        }
        
        [HttpPost("add-productdetail")]
        public async Task<IActionResult> Add([FromBody] ProductDetailDto productDetail){
            ProductDetail pdetail = new ProductDetail();
            pdetail.Id = productDetail.Id;
            pdetail.ProductId = productDetail.ProductId;
            pdetail.InitialPrice = productDetail.InitialPrice;
            pdetail.Price = productDetail.Price;
            pdetail.Color = productDetail.Color;
            pdetail.Size = productDetail.Size;
            _productDetailRepository.AddItem(pdetail);
            var result = await _productDetailRepository.UnitOfWork.SaveAsync();
            if(result> 0){
                return Ok();
            }
            else return BadRequest();
        }
        [HttpPost("Update-productDetail")]
        public async Task<IActionResult> Update([FromBody] ProductDetailDto request, CancellationToken cancellationToken){
            ProductDetail pdetail = new ProductDetail();
            pdetail.Id = request.Id;
            pdetail.ProductId = request.ProductId;
            pdetail.InitialPrice = request.InitialPrice;
            pdetail.Price = request.Price;
            pdetail.Color = request.Color;
            pdetail.Size = request.Size;
            
            _productDetailRepository.UpdateItem(pdetail);
            var result = await _productDetailRepository.UnitOfWork.SaveAsync();
            if(result> 0){
                return Ok();
            }
            else return BadRequest();
        }
        [HttpDelete("Delete-product-detail")]
        public async Task<IActionResult> Delete([FromBody] int id){
            if(id.ToString() is null){
                return BadRequest();

            }
            ProductDetail productDetail = await _productDetailRepository.GetItembyId(id);
            if(productDetail is null){
                return BadRequest();
            }
            _productDetailRepository.DeleteItem(productDetail);
            var result = await _productDetailRepository.UnitOfWork.SaveAsync();
            if(result> 0){
                return Ok();
            }
            return BadRequest();
        } 
        [HttpGet("GetItem-of-product")]
        public async Task<IEnumerable<ProductDetail>> GetItembyIdProduct(int id){
            return await _productDetailRepository.GetAllItemByIdProduct(id);
        }
        [HttpGet("GetItem")]
        public async Task<ProductDetail> GetItembyID(int id){
            return await _productDetailRepository.GetItembyId(id);
        }

    }
}