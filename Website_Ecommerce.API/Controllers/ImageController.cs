using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Semester7.PBL6.Website_Ecommerce.API.ModelDtos;
using Semester7.PBL6.Website_Ecommerce.API.Repositories;
using Website_Ecommerce.API.Data.Entities;

namespace Semester7.PBL6.Website_Ecommerce.API.Controllers
{   
    [Route("api/controller")]
    [ApiController]
    public class ImageController : ControllerBase   
    {
        private readonly IImageProduct _productImageRepository;
        private readonly IMapper _mapper;
        public ImageController(IImageProduct productImageRepository, IMapper mapper){
            _productImageRepository = productImageRepository;
            _mapper = mapper;
        }
        [HttpPost("Add-Image")]
        public async Task<IActionResult> Add([FromBody]ImageProductDto request, CancellationToken cancellationToken){
            var imageModel = _mapper.Map<ProductImage>(request);
            _productImageRepository.Add(imageModel);
            var result = await _productImageRepository.UnitOfWork.SaveAsync();
            if(result > 0){
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("Update-Image")]
        public async Task<IActionResult> Update([FromBody]ImageProductDto request,  CancellationToken cancellationToken){
            var imageModel = _mapper.Map<ProductImage>(request);
            _productImageRepository.Update(imageModel);
            var result = await _productImageRepository.UnitOfWork.SaveAsync();
            if(result > 0){
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete("Del-Image")]
        public async Task<IActionResult> Delete([FromBody] int id){
            if(id.ToString() is null){
                return BadRequest();
            }
            var item = await _productImageRepository.GetImagebyId(id);
            if(item is null){
                return BadRequest();
            }
            _productImageRepository.Delete(id);

            var result = await _productImageRepository.UnitOfWork.SaveAsync();
            if(result > 0){
                return Ok();
            }
            return BadRequest();
        }

        // [HttpGet("GetImage-of-Product/{id_product}")]

        // public async Task<IEnumerable<ImageProductDto>> GetImagebyIdProduct(int idProduct){
        // }
    }
}