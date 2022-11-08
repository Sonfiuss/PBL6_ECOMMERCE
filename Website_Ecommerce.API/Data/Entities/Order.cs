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
        public int State { get; set; }

        [Required]
        [MaxLength(256)]
        public string Address { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime SendDate { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Voucher")]
        public int VoucherId { get; set; } 
        public VoucherOrder VoucherOrder { get; set; }

        public Shipper Shipper { get; set; }
        [Required]
        public Payment Payment { get; set; }
        public IList<OrderDetail> OrderDetails { get; set; }
    }
}