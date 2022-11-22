using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.services
{
    public class OrderService : IOrderService
    {
        public bool CheckVoucherOrderMatch(Order order, VoucherOrder voucher)
        {
            if(voucher.Expired < DateTime.Now && voucher.MinPrice < order.TotalPrice){
                return false;
            }
            return true;
        }

        public bool CheckVoucherProduct(ProductDetail productDetail, VoucherProduct voucher)
        {
            throw new NotImplementedException();
        }
    }
}