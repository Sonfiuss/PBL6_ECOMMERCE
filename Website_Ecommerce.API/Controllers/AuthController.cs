using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_Ecommerce.API.Data;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.Enum;
using Website_Ecommerce.API.ModelDtos;
using Website_Ecommerce.API.Repositories;
using Website_Ecommerce.API.Response;
using Website_Ecommerce.API.services;

namespace Website_Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityServices _identityServices;

        public AuthController(
            IUserRepository userRepository, 
            IIdentityServices identityServices)
        {
            _userRepository = userRepository;
            _identityServices = identityServices;
        }
        
        

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request, CancellationToken cancellationToken)
        {
            User isExist = await _userRepository.Users.FirstOrDefaultAsync(x => x.Username == request.Username || x.Email == request.Email);

            if (isExist != null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.ExistUserOrEmail
                });
            }
            var check = false;
            if (request.Role == 1) 
            {
                check = true;
            }
            
            var user = new User {
                Username = request.Username,
                Email = request.Email,
                Gender = request.Gender,
                IsBlock = check,
                Password = _identityServices.GetMD5(request.Password)
            };

            _userRepository.Add(user);

            await _userRepository.UnitOfWork.SaveAsync(cancellationToken);

            var userRole = new UserRole()
            {
                UserId = user.Id,
                RoleId = request.Role
            };

            _userRepository.Add(userRole);

            var result = await _userRepository.UnitOfWork.SaveAsync(cancellationToken);

            if (result > 0)
            {
                return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = user.Id.ToString()
                    }
                });
            }
            else
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.BadRequest
                });
            }
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<Response<ResponseToken>> Login(LoginDto request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.Users.FirstOrDefaultAsync(x => x.Username == request.Username);

            if (user == null)
            {
                return new Response<ResponseToken>()
                {
                    State = false,
                    Message = ErrorCode.NotFound
                };
            }

            
            if (_identityServices.VerifyMD5Hash(user.Password, _identityServices.GetMD5(request.Password)))
            {
                int timeOut = 60 * 60 *24;

                List<int> roleIds = _userRepository.UserRoles
                    .Where(x => x.UserId == user.Id).Select(x => x.RoleId).ToList();

                string token = _identityServices.GenerateToken(
                    user.Id,user.Username,
                    roleIds,
                    timeOut);

                return new Response<ResponseToken>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseToken()
                    {
                        Token = token,
                    }
                };
            }
            else
            {
                return new Response<ResponseToken>()
                {
                    State = false,
                    Message = ErrorCode.BadRequest,
                };
            }
        }

        
        
    }
}