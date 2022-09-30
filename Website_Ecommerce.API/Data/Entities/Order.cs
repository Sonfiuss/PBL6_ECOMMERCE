using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime CreateDate { get; set; }
        public string Note { get; set; }

        // [ForeignKey("Shipper")]
        // public int ShipperId { get; set; }

        public IList<OrderDetail> OrderDetails { get; set; }
    }
}