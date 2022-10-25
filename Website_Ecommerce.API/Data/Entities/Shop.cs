using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        
        [MaxLength(256)]
        public string Address { get; set; }
        [MaxLength(128)]
        public string Email { get; set; }
        public int UrlAvatar { get; set; }
        [Required]
        public string Phone { get; set; }
        public int TotalCategory { get; set; }
        public int Rate { get; set; }
        public int TotalRate{ get; set; }
        [Required]
        public int Status { get; set; }

        public ICollection<Product> Products { get; set; }
        
    }
}