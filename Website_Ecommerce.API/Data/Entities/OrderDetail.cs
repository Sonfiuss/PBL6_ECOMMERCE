using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public List<Order> Orders { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public List<Product> Products { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }

        [ForeignKey("Coupon")]
        public int CounponId { get; set; }
        public Coupon Coupon { get; set; } 

    }
}