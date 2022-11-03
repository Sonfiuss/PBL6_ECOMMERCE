
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class Shipper
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
