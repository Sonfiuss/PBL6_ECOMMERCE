using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.ModelDtos;
using Website_Ecommerce.API.Repositories;
using Website_Ecommerce.API.Response;
using Website_Ecommerce.API.services;

namespace Website_Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "MyAuthKey")]
    // [CustomAuthorize(Allows = "2")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategroyRepository _categroyRepository;
        private readonly IMapper _mapper;

        public CategoryController(
            ICategroyRepository categroyRepository,
            IMapper mapper)
        {
            _categroyRepository = categroyRepository;
            _mapper = mapper;
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto request, CancellationToken cancellationToken)
        {
            Category category = new Category();
            category.Name = request.Name;
            _categroyRepository.Add(category);
            var result = await _categroyRepository.UnitOfWork.SaveAsync(cancellationToken);

            if (result > 0)
            {
                return Ok(new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = category.Id.ToString()
                    }
                });
            }
            return BadRequest(new Response<ResponseDefault>()
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
            if (category == null)
            {
                return BadRequest(new Response<ResponseDefault>()
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

            if (result > 0)
            {
                return Ok(new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = category.Id.ToString()
                    }
                });
            }
            return BadRequest(new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Update category fail"
                }
            });
        }

        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            Category category = _categroyRepository.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return BadRequest(new Response<ResponseDefault>()
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
            var result = await _categroyRepository.UnitOfWork.SaveAsync();

            if (result > 0)
            {
                return Ok(new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = category.Id.ToString()
                    }
                });
            }
            return BadRequest(new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Delete category fail"
                }
            });
        }

        [HttpGet("list-category")]

        public async Task<IActionResult> GetListCategory()
        {
            var categories = await _categroyRepository.Categories.Select(x => new Category { Id = x.Id, Name = x.Name }).ToListAsync();

            return Ok(new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = categories
                }
            });
        }

        [HttpGet("get-category-by/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            return Ok(await _categroyRepository.Categories
                .Select(x => new { x.Id, x.Name })
                .FirstOrDefaultAsync(x => x.Id == id)
            );
        }
    }
}