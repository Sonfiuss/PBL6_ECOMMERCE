using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.services
{
    public interface IOrderService
    {
        // param id Item in Cart
        void AddItemFromCart(int id);
        // param: id Product Detail 
        void AddItemDirect(int id, int amount);
    }
}