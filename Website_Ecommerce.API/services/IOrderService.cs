using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.services
{
    public interface IOrderService
    {   /// <summary>
        /// <param name = "voucherID">
        /// <summary>
        
        void AddVoucherOfShop(int id);
        
        void AddVoucherOrder(int id);
    }
}