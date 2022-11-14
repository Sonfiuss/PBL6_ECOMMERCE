using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.services
{
    public class OrderService : IOrderService
    {

        public void AddItemDirect(int id, int amount)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
        }

        public void AddItemFromCart(int id)
        {
            throw new NotImplementedException();
        }

        public void AddVoucherOfShop(int id)
        {
            throw new NotImplementedException();
        }

        public void AddVoucherOrder(int id)
        {
            throw new NotImplementedException();
        }
    }
}