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
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int UrlAvatar { get; set; }
        public string Phone { get; set; }
        public int TotalCategory { get; set; }
        public int Rate { get; set; }
        public int TotalRate{ get; set; }
        public int Status { get; set; }

        public ICollection<Product> Products { get; set; }
        
    }
}