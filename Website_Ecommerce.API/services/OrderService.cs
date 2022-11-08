using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.services
{
    public class OrderService : IOrderService
    {
        public void AddItemDirect(int id, int amount)
        {
                
        }

        public void AddItemFromCart(int id)
        {
            throw new NotImplementedException();
        }
    }
}