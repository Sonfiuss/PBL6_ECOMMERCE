using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.Repositories;
using Website_Ecommerce.API.Response;

namespace Website_Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "MyAuthKey")]
    public class AdminController : ControllerBase
    {
        private readonly IShopRepository _shopRepository;
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public AdminController(
            IShopRepository shopRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IHttpContextAccessor httpContext)
        {
            _productRepository = productRepository;
            _shopRepository = shopRepository;
            _httpContext = httpContext;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        } 

        //Get list all user
        [HttpGet("get-list-user")]
        public async Task<IActionResult> GetListUser()
        {
            
            List<User> users = await _userRepository.Users.ToListAsync();
            if(users == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "Not Found User"
                    }
                });
            }

            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = users
                }
            });
        }

        //Get list Shop
        [HttpGet("get-list-shop-active")]
        public async Task<IActionResult> GetListShopActive()
        {
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());
            //check userId
            List<Shop> shops = await _shopRepository.Shops.Where(x => x.Status == true).ToListAsync();
            if(shops == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "Not Found Shop"
                    }
                });
            }

            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = shops
                }
            });
        }

        [HttpGet("get-list-shop-no-active")]
        public async Task<IActionResult> GetListShopNoActive()
        {
            List<Shop> shops = await _shopRepository.Shops.Where(x => x.Status == false).ToListAsync();
            if(shops == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "Not Found Shop"
                    }
                });
            }

            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = shops
                }
            });
        }
    }
}