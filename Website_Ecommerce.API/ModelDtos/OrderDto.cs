using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.ModelDtos
{
    public class OrderDto
    {

        public int Id { get; set; }
        public int State { get; set; }
        public string Address { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime SendDate { get; set; }
        public int UserId { get; set; }
        public int VoucherId { get; set; } 
        public int paymentMethodId {get; set;}
        public IList<ItemOrderDto> ItemOrderDtos { get; set; }
        
        // <summary>
        // Map from Model Order to Order Dto
        // </summary>
        public OrderDto ToOrderDto(Order order){
            var orderDto = new OrderDto{
                Id = order.Id,
                UserId = order.UserId,
                VoucherId = order.VoucherId,
                State = order.State,
                Address = order.Address,
                CreateDate = order.CreateDate,
                SendDate = order.SendDate
            };
            foreach (var i in order.OrderDetails)
            {
                orderDto.ItemOrderDtos.Add(new ItemOrderDto{
                    ProductDetailId = i.ProductDetailId,
                    VoucherProductId = i.VoucherProductId,
                    Amount = i.Amount,
                    Price = i.Price,
                    Note = i.Note
                });
            }
            return orderDto;
        }
    }
    

    public class ItemOrderDto
    {
        public int OrderId { get; set; }
        public int ProductDetailId { get; set; }
        public int VoucherProductId { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public string Note { get; set; }
    }
    

}
