using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CommentController(
            ICommentRepository commentRepository,
            IHttpContextAccessor httpContext,
            IProductRepository productRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _httpContext = httpContext;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("add-comment")]
        public async Task<IActionResult> AddComment([FromBody] CommentDto request, CancellationToken cancellationToken)
        {
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());

            var existComment = await _commentRepository.Comments.FirstOrDefaultAsync(x => x.ProductId == request.ProductId && x.UserId == userId);
            if(existComment != null)
            {
                return BadRequest(new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.ExistedDB,
                    Result = new ResponseDefault()
                    {
                        Data = "Ban da danh gia roi"
                    }
                });
            }

            Comment comment = new Comment();
            comment.Content = request.Content;
            comment.UserId = userId;
            comment.ProductId = request.ProductId;
            // 0 is delete, 1 is ton tai
            comment.State = 1;
            comment.Rate = request.Rate;
            _commentRepository.Add(comment);
            var result = await _commentRepository.UnitOfWork.SaveAsync(cancellationToken);

            if (result > 0)
            {
                return Ok(new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = comment.Id.ToString()
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



        [HttpPut("update-comment")]
        public async Task<IActionResult> UpdateComment([FromBody] CommentDto request, CancellationToken cancellationToken)
        {
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());

            Comment comment = _commentRepository.Comments.FirstOrDefault(c => c.Id == request.Id && c.UserId == userId);
            if (comment == null)
            {
                return BadRequest(new Response<ResponseDefault>()
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
            comment.Rate = request.Rate;
            _commentRepository.Update(comment);
            var result = await _commentRepository.UnitOfWork.SaveAsync(cancellationToken);

            if (result > 0)
            {
                return Ok(new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = comment.Id.ToString()
                    }
                });
            }
            return BadRequest(new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Update Comment fail"
                }
            });
        }

        [HttpPut("delete-comment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());

            Comment comment = _commentRepository.Comments.FirstOrDefault(c => c.Id == id && c.UserId == userId);
            if (comment == null)
            {
                return BadRequest(new Response<ResponseDefault>()
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

            if (result > 0)
            {
                return Ok(new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = comment.Id.ToString()
                    }
                });
            }
            return BadRequest(new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Delete Comment fail"
                }
            });
        }

        [HttpGet("list-comment-by/{id}")]
        public async Task<IActionResult> GetListComment(int productId)
        {
            // var listcomments = await _commentRepository.Comments.Where(x => x.State == 1).Select(x => new {x.Id, x.UserId, x.Content}).ToListAsync();
            var listCommentDetails = await _commentRepository.Comments.Where(x => x.State == 1).Join(_userRepository.Users, c => c.UserId, u => u.Id,
                                                                        (c, u) => new
                                                                        {
                                                                            Id = c.Id,
                                                                            Content = c.Content,
                                                                            Username = u.Username,
                                                                            Avatar = u.UrlAvatar
                                                                        }).ToListAsync();

            return Ok(new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = listCommentDetails
                }
            });
        }


    }
}