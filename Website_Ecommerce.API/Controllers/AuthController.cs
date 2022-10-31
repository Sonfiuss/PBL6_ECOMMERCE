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

        public AuthController(IUserRepository userRepository, IIdentityServices identityServices)
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
            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(request.Password);
            var user = new User {
                Username = request.Username,
                Email = request.Username + "@gmail.com",
                Gender = request.Gender,
                IsBlock = check,
                PasswordSalt = hmac.Key,
                Password = hmac.ComputeHash(passwordBytes)
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
        public async Task<IActionResult> Login(LoginDto request, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.Users.FirstOrDefaultAsync(x => x.Username == request.Username);

            if (currentUser == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound
                });
            }

            using var hmac = new HMACSHA512(currentUser.PasswordSalt);
            var passwordBytes = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(request.Password)
            );
            //check tung byte cua password voi 
            for(int i = 0; i < currentUser.Password.Length; i++)
            {
                if(currentUser.Password[i] != passwordBytes[i])
                {
                    return BadRequest( new Response<ResponseDefault>()
                    {
                        State = false,
                        Message = ErrorCode.BadRequest
                    });
                }
            }
            
            //lay role cua user hien tai
            List<string> roleIds = _userRepository.UserRoles
                .Where(x => x.UserId == currentUser.Id).Select(x => x.Role.Name).ToList();

            string token = _identityServices.CreateToken(
                currentUser.Id,
                currentUser.Username,
                roleIds
                );

            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = "Bearer " + token,
                }
            });
        }

        
        
    }
}