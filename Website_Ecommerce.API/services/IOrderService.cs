using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.services
{
    public interface IOrderService
    {   /// <summary>
        bool CheckVoucherOrderMatch(Order order, VoucherOrder voucher);
        bool CheckVoucherProduct(ProductDetail productDetail, VoucherProduct voucher);
    }
}