using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IShopRepository _shopRepository;
        private readonly IMapper _mapper;

        public UserController(
            IUserRepository userRepository,
            IHttpContextAccessor httpContext,
            IShopRepository shopRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _httpContext = httpContext;
            _shopRepository = shopRepository;
            _mapper = mapper;
        }


        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileDto request, CancellationToken cancellationToken)
        {
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());

            User user = _userRepository.Users.FirstOrDefault(x => x.Id == userId);

            // user = _mapper.Map< ProfileDto, User>(request);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Gender = request.Gender;
            user.Email = request.Email;
            user.DateOfBirth = request.DateOfBirth;
            user.UrlAvatar = request.UrlAvatar;
            user.Phone = request.Phone;

            _userRepository.Update(user);

            var result = await _userRepository.UnitOfWork.SaveAsync(cancellationToken);

            if (result > 0)
            {
                return Ok(new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = user.Id.ToString()
                    }
                });
            }
            return BadRequest(new Response<ResponseDefault>()
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
        public async Task<IActionResult> RequestRoleShop([FromBody] ShopDto request, CancellationToken cancellationToken)
        {
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());

            var user = _userRepository.Users.FirstOrDefault(x => x.Id == userId);

            Shop shop = new Shop();
            shop = _mapper.Map<ShopDto, Shop>(request);
            shop.Email = user.Email;
            shop.Status = false;
            //totalrate = -1 //confirm role shop
            shop.TotalRate = -1;
            shop.AverageRate = 0;
            shop.UserId = userId;
            shop.Status = false;
            _shopRepository.Add(shop);

            var result = await _shopRepository.UnitOfWork.SaveAsync(cancellationToken);

            if (result == 0)
            {
                return BadRequest(new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.ExcuteDB,
                    Result = new ResponseDefault()
                    {
                        Data = "Add shop fail"
                    }
                });
            }

            return Ok(new Response<ResponseDefault>()
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