using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class VoucherOrder
    {
        [Key]
        public int Id { get; set; }
        public double Value { get; set; }
        public double MinPrice { get; set; }
        public int Amount { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}