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
    public class CategoryController : ControllerBase
    {
        private readonly ICategroyRepository _categroyRepository;

        public CategoryController(ICategroyRepository categroyRepository)
        {
            _categroyRepository = categroyRepository;
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto request, CancellationToken cancellationToken)
        {
            Category category = new Category();
            category.Name = request.Name;
            _categroyRepository.Add(category);
            var result = await _categroyRepository.UnitOfWork.SaveAsync(cancellationToken);

            if(result > 0)
            {
                return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = category.Id.ToString()
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


        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto request, CancellationToken cancellationToken)
        {
            Category category = _categroyRepository.Categories.FirstOrDefault(c => c.Id == request.Id);
            if(category == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "Update category fail"
                    }
                });
            }

            category.Name = request.Name;
            _categroyRepository.Update(category);
            var result = await _categroyRepository.UnitOfWork.SaveAsync(cancellationToken);

            if(result > 0)
            {
                return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = category.Id.ToString()
                    }
                });
            }
            return BadRequest( new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Update category fail"
                }
            });
        }
        
        [HttpDelete("delete-category")]
        public async Task<IActionResult> DeleteCategory([FromQuery] int Id, CancellationToken cancellationToken)
        {
            Category category = _categroyRepository.Categories.FirstOrDefault(c => c.Id == Id);
            if(category == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "Delete category fail"
                    }
                });
            }

            _categroyRepository.Delete(category);
            var result = await _categroyRepository.UnitOfWork.SaveAsync(cancellationToken);

            if(result > 0)
            {
                return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = category.Id.ToString()
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