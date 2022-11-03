
using Microsoft.AspNetCore.Mvc;
using Semester7.PBL6.Website_Ecommerce.API.ModelDtos.CartDto;
using Semester7.PBL6.Website_Ecommerce.API.ModelDtos.CartDtoame;
using Semester7.PBL6.Website_Ecommerce.API.Repositories;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.Response;

namespace Semester7.PBL6.Website_Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        public CartController(ICartRepository cartRepository){
            _cartRepository = cartRepository;
        }
        [HttpPost("AddCartItem")]
        public async Task<IActionResult> AddCartItem([FromBody] CustomerShoppingCart request, CancellationToken cancellationToken){

            CartItem cartitem = new CartItem{Id = request.Id};
            cartitem.Id = request.Id;
            cartitem.ProductDetailId = request.ProductDetailId;
            cartitem.UserId = request.UserId;
            cartitem.Amount = request.Amount;
            _cartRepository.AddItem(cartitem);

            var result = await _cartRepository.UnitOfWork.SaveAsync(cancellationToken);

            if(result >0){
                return Ok(new Response<ResponseDefault>(){
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault(){
                        Data = "Add Item success"
                    }
                });
            }
            return BadRequest();

        }
        [HttpDelete("DeleteItem/{id}")]
        public async Task<IActionResult> DeleteIteminCart([FromQuery] int id){
            if(id.ToString() is null){
                return BadRequest();
            }
            CartItem item = _cartRepository.GetItembyID(id);
            if(item is null){
                return BadRequest(new Response<ResponseDefault>()
                {
                        State = false,
                        Message = ErrorCode.NotFound,
                        Result = new ResponseDefault()
                        {
                            Data = "NotFound Product"
                        }
                });
            }
            _cartRepository.RemoveItem(id);
            var result = await _cartRepository.UnitOfWork.SaveAsync();
            if(result > 0){
                return Ok(new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = item.Id.ToString()
                    }
                });
            }
            return BadRequest( new Response<ResponseDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                Result = new ResponseDefault()
                {
                    Data = "Delete Item fail"
                }
            });
        }
        [HttpPost("/api/UpdateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] CustomerShoppingCart request, CancellationToken cancellationToken){
            var item = _cartRepository.Items.FirstOrDefault(p => p.Id == request.Id);
            if(item is null){
                return BadRequest();
            }
            item.ProductDetailId = request.ProductDetailId;
            item.Amount = request.Amount;
            var result = await _cartRepository.UnitOfWork.SaveAsync(cancellationToken);
            if(result <= 0){
                return BadRequest();
            }
            return Ok();

        }
        [HttpGet("GetItem/{idUser}")]
        
        public async Task<IEnumerable<ReadItemDto>> GetItem(int id){
            return await _cartRepository.GetAllItemByIdUser(id);
        }
    }
}