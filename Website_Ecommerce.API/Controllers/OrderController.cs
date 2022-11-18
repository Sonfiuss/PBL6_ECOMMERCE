using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IVoucherOrderRepository _voucherOrderRepository;
        private readonly IShopRepository _shopRepository;
        public OrderController(IOrderRepository orderRepository,
                IHttpContextAccessor httpContext,
                IProductRepository productRepository,
                IShopRepository shopRepository,
                IVoucherOrderRepository voucherOrderRepository){
            _shopRepository = shopRepository;
            _voucherOrderRepository = voucherOrderRepository;
            _orderRepository = orderRepository;
            _httpContext = httpContext;
            _productRepository = productRepository;
        }
        [HttpPost("Add-Order")]
        public async Task<IActionResult> AddOrder([FromBody] OrderDto request, CancellationToken cancellationToken){
            int userId = int.Parse(_httpContext.HttpContext.User.Identity.Name.ToString());
            if(request.VoucherId == 0){
                request.VoucherId = 1;
            }
            VoucherOrder voucherOrder = _voucherOrderRepository.VoucherOrders.FirstOrDefault(x => x.Id == request.VoucherId);
            

            var totalPrice = 0.0;
            foreach(var i in request.ItemOrderDtos){
                if(i.VoucherProductId != 0){
                    VoucherProduct voucherProduct = _shopRepository.voucherProducts.FirstOrDefault(x => x.Id == i.VoucherProductId);
                    totalPrice += i.Price * i.Amount - voucherProduct.Value;
                }
                else{
                    totalPrice += i.Price * i.Amount;
                }
            }
            var order = new Order{
                Id = request.Id,
                UserId = userId,
                Address = request.Address,
                CreateDate = DateTime.Now,
                VoucherId = request.VoucherId,
                State = (int)StateOrderEnum.SENT

            };
            if(voucherOrder.MinPrice > totalPrice){
                order.VoucherId = 1;
            }
            if(voucherOrder.Id == 1){

            }
            else{
            voucherOrder.Amount = voucherOrder.Amount - 1;
            }
            _orderRepository.Add(order);
            _voucherOrderRepository.Update(voucherOrder);
            var result = await  _orderRepository.UnitOfWork.SaveAsync(cancellationToken);
            if(result <=0){
                 return BadRequest( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.BadRequest,
                    Result = new ResponseDefault()
                    {
                        Data = ""
                    }
                });
            }
            Order lastOrder = _orderRepository.Orders.OrderByDescending(x => x.Id).FirstOrDefault(x => x.UserId == userId);

            foreach(var i in request.ItemOrderDtos){
                ProductDetail productDetail = _productRepository.ProductDetails.FirstOrDefault(x => x.Id == i.ProductDetailId);
                Product product = _productRepository.Products.FirstOrDefault(x => x.Id == productDetail.ProductId);
                Shop shop = _shopRepository.Shops.FirstOrDefault(x => x.Id == product.ShopId);
                if(i.VoucherProductId == 0){
                    i.VoucherProductId = 1;
                }
                VoucherProduct voucherProduct = _shopRepository.voucherProducts.FirstOrDefault(x => x.Id == i.VoucherProductId);

                var orderDetail = new OrderDetail{
                    OrderId = lastOrder.Id,
                    ProductDetailId = i.ProductDetailId,
                    Amount = i.Amount,
                    ShopId = shop.Id,
                    State = (int)StateOrderDetailEnum.UNCONFIRMED,
                    Price = productDetail.Price * i.Amount,
                    VoucherProductId =  i.VoucherProductId
                };
                _orderRepository.Add(orderDetail);
                productDetail.Amount = productDetail.Amount - orderDetail.Amount;
                if(orderDetail.VoucherProductId != 1){
                    voucherProduct.Amount = voucherProduct.Amount - 1;
                }
                _shopRepository.Update(voucherProduct);
                _productRepository.Update(productDetail);
                var resultI = await  _orderRepository.UnitOfWork.SaveAsync(cancellationToken);
                if(resultI <= 0 ){
                    _orderRepository.Delete(lastOrder);
                    return BadRequest( new Response<ResponseDefault>()
                    {
                        State = true,
                        Message = ErrorCode.BadRequest,
                        Result = new ResponseDefault()
                        {
                            Data = ""
                        }
                    });
                }
            }
           return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = ""
                    }
                });
        }
        [HttpPatch("Cancel-order-detail")]
        public async Task<IActionResult> CancelOrder(int orderId, int productDetailId, CancellationToken cancellationToken){
            var orderDetail = await  _orderRepository.OrderDetails.FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductDetailId == productDetailId);
            if(orderDetail == null){
                 return BadRequest( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = ""
                    }
                });
            }
            if(orderDetail.State > (int)StateOrderEnum.RECEIVED){
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = "Khong the huy don hang"
                    }
                });
            }
            orderDetail.State = (int)StateOrderEnum.CANCALLED;
            _orderRepository.Update(orderDetail);
            var result = await  _orderRepository.UnitOfWork.SaveAsync(cancellationToken);
            if(result == 0){
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = "Khong the huy don hang"
                    }
                });
            }
            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = "Da Huy don hang"
                }
            });

            
        }
        
    }
    
}