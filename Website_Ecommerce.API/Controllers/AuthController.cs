using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website_Ecommerce.API.Data;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.ModelDtos;
using Website_Ecommerce.API.services;

namespace Website_Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _datacContext;
        private readonly ITokenService _tokenService;

        public AuthController(DataContext dataContext, ITokenService tokenService)
        {
            _datacContext = dataContext;
            _tokenService = tokenService;
        }
        
        

        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthUserDto authUserDto)
        {
            authUserDto.Username = authUserDto.Username.ToLower();
            if (_datacContext.Users.Any(u => u.Username == authUserDto.Username))
            {
                return BadRequest("Username is already existed!");
            }
 
            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(authUserDto.Password);
            var user = new User {
                Username = authUserDto.Username,
                PasswordSalt = hmac.Key,
                PasswordHash = hmac.ComputeHash(passwordBytes)
            };
 
            _datacContext.Users.Add(user);
            _datacContext.SaveChanges();
            var token = _tokenService.CreateToken(user.Username);
            return Ok(token);
            
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthUserDto authUserDto)
        {
            authUserDto.Username = authUserDto.Username.ToLower();

            var currentUser = _datacContext.Users.FirstOrDefault(x => x.Username == authUserDto.Username);

            if(currentUser == null)
            {
                return Unauthorized("Username is invalid.");
            }

            using var hmac = new HMACSHA512(currentUser.PasswordSalt);
            var passwordBytes = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(authUserDto.Password)
            );

            for(int i = 0; i < currentUser.PasswordHash.Length; i++)
            {
                if(currentUser.PasswordHash[i] != passwordBytes[i])
                {
                    return Unauthorized("Password is valid.");
                }
            }
            
            var token = _tokenService.CreateToken(currentUser.Username);
            return Ok(token);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_datacContext.Users.ToList());
        }
    }
}