using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.ModelDtos;
using Website_Ecommerce.API.Repositories;
using Website_Ecommerce.API.Response;

namespace Website_Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "MyAuthKey")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CommentController(
            ICommentRepository commentRepository,
            IHttpContextAccessor httpContext,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _httpContext = httpContext;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        
        [HttpPost("add-comment")]
        public async Task<IActionResult> AddComment([FromBody] CommentDto request, CancellationToken cancellationToken)
        {
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());

            Comment comment = new Comment();
            comment.Content = request.Content;
            comment.UserId = userId;
            comment.ProductId = request.ProductId;
            comment.State = 1; //ton tai
            _commentRepository.Add(comment);
            var result = await _commentRepository.UnitOfWork.SaveAsync(cancellationToken);

            if(result > 0)
            {
                return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = comment.Id.ToString()
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
        


        [HttpPut("update-comment")]
        public async Task<IActionResult> UpdateComment([FromBody] CommentDto request, CancellationToken cancellationToken)
        {
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());

            Comment comment = _commentRepository.Comments.FirstOrDefault(c => c.Id == request.Id && c.UserId == userId);
            if(comment == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "Update Comment fail"
                    }
                });
            }

            comment.Content = request.Content;
            _commentRepository.Update(comment);
            var result = await _commentRepository.UnitOfWork.SaveAsync(cancellationToken);

            if(result > 0)
            {
                return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = comment.Id.ToString()
                    }
                });
            }
            return BadRequest( new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Update Comment fail"
                }
            });
        }
        
        [HttpPut("delete-Comment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());

            Comment comment = _commentRepository.Comments.FirstOrDefault(c => c.Id == id && c.UserId == userId);
            if(comment == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "Delete Comment fail"
                    }
                });
            }

            comment.State = 0;
            _commentRepository.Update(comment);
            var result = await _commentRepository.UnitOfWork.SaveAsync();

            if(result > 0)
            {
                return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = comment.Id.ToString()
                    }
                });
            }
            return BadRequest( new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Delete Comment fail"
                }
            });
        }

        [HttpGet("list-Comment-by/{productId}")]

        public async Task<IActionResult> GetListComment(int productId)
        {
            var categories = _commentRepository.Comments.Where(x => x.State == 1).Select(x => x.Content).ToList();

            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = categories
                }
            });
        }

        
    }
}