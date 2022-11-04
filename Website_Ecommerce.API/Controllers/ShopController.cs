using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.ModelDtos;
using Website_Ecommerce.API.Repositories;
using Website_Ecommerce.API.Response;

namespace Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopRepository _shopRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUserRepository _userRepository;

        public ShopController(
            IShopRepository shopRepository,
            IUserRepository userRepository,
            IHttpContextAccessor httpContext)
        {
            _shopRepository = shopRepository;
            _httpContext = httpContext;
            _userRepository = userRepository;
        }

        [HttpPost("add-shop")]
        public async Task<IActionResult> AddShop(ShopDto request, CancellationToken cancellationToken)
        {
            string userName = _httpContext.HttpContext.User.Identity.Name.ToString();
            var user = _userRepository.Users.FirstOrDefault(x => x.Username == userName);

            Shop shop = new Shop();
            shop.Name = request.Name;
            shop.Address = request.Address;
            shop.Email = user.Email; //get shopid from token
            shop.Phone = request.Phone;
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


        [HttpPost("update-shop")]
        public async Task<IActionResult> UpdateShop(ShopDto request, CancellationToken cancellationToken)
        {
            string userName = _httpContext.HttpContext.User.Identity.Name.ToString();
            var user = _userRepository.Users.FirstOrDefault(x => x.Username == userName);

            Shop shop = new Shop();
            shop.Name = request.Name;
            shop.Address = request.Address;
            shop.Email = user.Email; //get shopid from token
            shop.Phone = request.Phone;
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

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}