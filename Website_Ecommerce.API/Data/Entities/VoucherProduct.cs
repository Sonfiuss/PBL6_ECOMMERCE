using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class VoucherProduct
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public double Value { get; set; }
        public double MinPrice { get; set; }
        public int Amount { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime Expired { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}