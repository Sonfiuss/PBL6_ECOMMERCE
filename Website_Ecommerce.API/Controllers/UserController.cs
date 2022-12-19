using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.ModelDtos;
using Website_Ecommerce.API.Repositories;
using Website_Ecommerce.API.Response;
using Website_Ecommerce.API.services;

namespace Website_Ecommerce.API.Controllers
{
    [Authorize(AuthenticationSchemes = "MyAuthKey")]

    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IShopRepository _shopRepository;

        public UserController(
            IUserRepository userRepository,
            IHttpContextAccessor httpContext,
            IShopRepository shopRepository)
        {
            _userRepository = userRepository;
            _httpContext = httpContext;
            _shopRepository = shopRepository;
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile(ProfileDto request, CancellationToken cancellationToken)
        {
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());

            User user = _userRepository.Users.FirstOrDefault(x => x.Id == userId);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Gender = request.Gender;
            user.Email = request.Email;
            user.DateOfBirth = request.DateOfBirth;
            user.UrlAvatar = request.UrlAvatar;
            user.Phone = request.Phone;

            _userRepository.Update(user);

            var result = await _userRepository.UnitOfWork.SaveAsync(cancellationToken);

            if(result > 0)
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
            return BadRequest( new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Update profile fail"
                }
            });
        }

        [HttpPost("request-role-shop")]
        public async Task<IActionResult> GetUSerId(ShopDto request, CancellationToken cancellationToken)
        {
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());

            var user = _userRepository.Users.FirstOrDefault(x => x.Id == userId);
            
            Shop shop = new Shop();
            shop.Name = request.Name;
            shop.Address = request.Address;
            shop.Email = user.Email; //get shopid from token
            shop.Phone = request.Phone;
            shop.Status = false;
            shop.TotalRate = 0;
            shop.AverageRate = 0;
            shop.UserId = userId;
            shop.Status = false;
            _shopRepository.Add(shop);
            var result = await _shopRepository.UnitOfWork.SaveAsync(cancellationToken);
            if(result == 0)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.ExcuteDB,
                    Result = new ResponseDefault()
                    {
                        Data = "Add shop fail"
                    }
                });
            }



            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = "Add shop success"
                }
            });
        }

    }
}