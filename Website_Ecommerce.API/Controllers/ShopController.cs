using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.Enum;
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
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public ShopController(
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
        #region "API Add/ update/ delete / getListShop"
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

        [HttpGet("get-shop-by-current-user")]
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

        
        #endregion
        #region "API Voucher Shop"
        [HttpPost("add-voucher-of-shop")]
        public async Task<IActionResult> AddVoucherShop(VoucherShopDto request, CancellationToken cancellationToken)
        {   
            request.CreateAt  = DateTime.Now;
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());

            var user = _userRepository.Users.FirstOrDefault(x => x.Id == userId);
            var shop = _shopRepository.Shops.FirstOrDefault(x => x.UserId == userId);

            VoucherProduct voucherProduct = _mapper.Map<VoucherProduct>(request);
            voucherProduct.ShopId = shop.Id;
            _shopRepository.Add(voucherProduct);
            var result = await _shopRepository.UnitOfWork.SaveAsync(cancellationToken);
            if(result == 0)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.ExcuteDB,
                    Result = new ResponseDefault()
                    {
                        Data = "Add voucher product fail"
                    }
                });
            }
            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = "Add voucher Product success"
                }
            });
        }
        [HttpPost("update-voucher-by-shop")]
        public async Task<IActionResult> UpdateVoucherShop(VoucherShopDto request, CancellationToken cancellationToken)
        {
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());

            
            var shop = _shopRepository.Shops.FirstOrDefault(x => x.UserId == userId);

            
            VoucherProduct voucher = _mapper.Map<VoucherProduct>(request);
            voucher.ShopId = shop.Id;

            _shopRepository.Update(voucher);
            var result = await _shopRepository.UnitOfWork.SaveAsync(cancellationToken);
            if(result == 0)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.ExcuteDB,
                    Result = new ResponseDefault()
                    {
                        Data = "Update Voucher fail!"
                    }
                });
            }
                        return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = "Update voucher Product success!"
                }
            });
        }
        [HttpGet("get-voucher-of-shop")]
        public async Task<IActionResult> GetVoucherOfShop(){
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());
            var shop = _shopRepository.Shops.FirstOrDefault(x => x.UserId == userId);
            
            List<VoucherProduct> list =  await _shopRepository.voucherProducts.Where(x => x.ShopId == shop.Id).ToListAsync();
            List<VoucherShopDto> listDto = _mapper.Map<List<VoucherShopDto>>(list);
             return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = listDto
                }
            });

        }
        [HttpGet("get-voucher-of-shop-by/{id}")]
        public async Task<IActionResult> GetVoucherbyId(int id){
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());
            var shop = _shopRepository.Shops.FirstOrDefault(x => x.UserId == userId);
            
            VoucherProduct voucherProduct =  await _shopRepository.voucherProducts.Where(x => x.ShopId == shop.Id && x.Id == id).FirstOrDefaultAsync();
            VoucherShopDto voucherProductDto = _mapper.Map<VoucherShopDto>(voucherProduct);
            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = voucherProductDto
                }
            });
        }

        [HttpGet("get-voucher-of-shop-match")]

        public async Task<IActionResult> GetVoucherShopMatch(int price){
            List<VoucherProduct> vouchers = await _shopRepository.voucherProducts.Where(p => p.MinPrice > price && p.Amount > 0).ToListAsync();
            if(vouchers.Count() <= 0){
            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = "Khong co voucher nao phu hop"
                }
            });
            }
            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = vouchers
                }
            });
        }
        #endregion
        #region "API Order of Shop"
        [HttpGet("get-list-order-detail-unconfirm-by-shop")]
        public async Task<IActionResult> GetListUnConfirmOrder(){
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());
            var shop = _shopRepository.Shops.FirstOrDefault(x => x.UserId == userId);
            var shopId = shop.Id;
            var listOrderDetailOfShop = await _orderRepository.OrderDetails
                                        .Where(r => r.ShopId == shopId && r.State == (int)StateOrderDetailEnum.UNCONFIRMED)
                                        .ToListAsync();
            if(listOrderDetailOfShop.Count ==0){
                 return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.BadRequest,
                    Result = new ResponseDefault()
                    {
                        Data = "Không có đơn hàng nào"
                    }
                });
            }
            return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.BadRequest,
                    Result = new ResponseDefault()
                    {
                        Data = listOrderDetailOfShop
                    }
                });
            
        }
        // [HttpGet("get-list-order-detail-of-shop")]
        // public async Task<IActionResult> getListOrderDetailOfShop(){
            
        //     return Ok( new Response<ResponseDefault>()
        //         {
        //             State = true,
        //             Message = ErrorCode.BadRequest,
        //             Result = new ResponseDefault()
        //             {
        //                 Data = orderDetails
        //             }
        //         });
            
        // }
        [HttpPatch("confirm-order")]
        public async Task<IActionResult> ConfirmOrder (int orderID, int productDetailId, int state, CancellationToken cancellationToken){
            var orderDetail = await _orderRepository.OrderDetails
                                .FirstOrDefaultAsync(r => r.OrderId == orderID && r.ProductDetailId == productDetailId);
            if(orderDetail == null){
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.BadRequest,
                    Result = new ResponseDefault()
                    {
                        Data = "Not found Order"
                    }
                }); 
            }
            
            orderDetail.State = state /*(int)StateOrderDetailEnum.CONFIRMED*/;
            if(state == 2){
                orderDetail.ShopConfirmDate = DateTime.Now;
            }
            else{
                orderDetail.ShopSendDate = DateTime.Now;
            }
            _orderRepository.Update(orderDetail);
            var result = await _orderRepository.UnitOfWork.SaveAsync(cancellationToken);
            if(result == 0){
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.BadRequest,
                    Result = new ResponseDefault()
                    {
                        Data = "Can't confirm item"
                    }
                });
            }
            return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.BadRequest,
                    Result = new ResponseDefault()
                    {
                        Data = "successful"
                    }
                });


        }
        #endregion
    }
}