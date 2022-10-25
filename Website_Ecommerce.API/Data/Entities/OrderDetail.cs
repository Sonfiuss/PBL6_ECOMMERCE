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
<<<<<<< HEAD
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public List<Order> orders { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public List<Product> products { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }

        [ForeignKey("Coupon")]
        public int CounponId { get; set; }
        public Coupon coupon { get; set; } 
=======
        [Column(Order = 1)]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        
        [Key]
        [Column(Order = 2)]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("VoucherShop")]
        public int VoucherProductId { get; set; }
        public VoucherProduct VoucherProduct { get; set; } 
        [Required]
        public int Amount { get; set; }
        [Required]
        public double Price { get; set; }
        [MaxLength(256)]
        public string? Note { get; set; }
>>>>>>> 31f0e805f63357d227287102869eabfb0c22e234

    }
}