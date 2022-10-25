using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }
        public int Percent { get; set; }
        public int ApplyType { get; set; }
        public int ApplyFor { get; set; }
    }
}