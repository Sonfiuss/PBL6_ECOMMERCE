using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPut("update-shop")]
        public async Task<IActionResult> UpdateShop(ShopDto request, CancellationToken cancellationToken)
        {
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());
            //check userId
            Shop shop = _shopRepository.Shops.FirstOrDefault(x => x.UserId == userId);
            if(shop == null)
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

            shop.Name = request.Name;
            shop.Address = request.Address;
            shop.Phone = request.Phone;
            shop.Status = false;
            _shopRepository.Update(shop);
            var result = await _shopRepository.UnitOfWork.SaveAsync(cancellationToken);
            if(result == 0)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.ExcuteDB,
                    Result = new ResponseDefault()
                    {
                        Data = "Update shop fail"
                    }
                });
            }



            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = "Update shop success"
                }
            });
        }

        [HttpDelete("delete-shop/{id}")]
        public async Task<IActionResult> DeleteShop(int id)
        {
            if(id.ToString() is null)
            {
                return BadRequest(null);
            }
            var shop = _shopRepository.Shops.FirstOrDefault(x => x.Id == id);
            if(shop == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "NotFound Shop"
                    }
                });
            }

            _shopRepository.Delete(shop);
            
            var result = await _shopRepository.UnitOfWork.SaveAsync();

            if(result > 0)
            {
                return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = shop.Id.ToString()
                    }
                });
            }
            return BadRequest( new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Delete category fail"
                }
            });
        }

        [HttpGet("get-shop-by-userid")]
        public async Task<IActionResult> GetShopById()
        {
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());
            //check userId
            Shop shop = await _shopRepository.Shops.FirstOrDefaultAsync(x => x.UserId == userId);
            if(shop == null)
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
                    Data = shop
                }
            });
        }

        [HttpGet("get-list-shop")]
        public async Task<IActionResult> GetListShop()
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

        

    }
}