using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Semester7.PBL6.Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Data.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Material { get; set; }
        [Required]
        public string Origin { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set;}

        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        public Shop Shop { get; set;}
        [Required]
        public int Status { get; set; }

        public ICollection<ProductDetail> ProductDetails { get; set; }
        public IList<OrderDetail> OrderDetails { get; set; }
        public ICollection<VoucherProduct> VoucherProducts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        
        
    }
}