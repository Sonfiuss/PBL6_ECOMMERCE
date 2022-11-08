using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.ModelDtos
{
    public class VoucherOrderDto
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public double MinPrice { get; set; }
        public int Amount { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime Expired { get; set; }
    }
    public class VoucherShopDto{
        public int ID { get; set; }
    }
}