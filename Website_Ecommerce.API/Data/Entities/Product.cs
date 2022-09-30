using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class Product
    {
        public Product()
        {
            ProductImages = new List<ProductImage>();
        }
        
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set;}


        [ForeignKey("ProductImage")]
        public int ProductImageId { get; set; }
        public List<ProductImage> ProductImages { get; set; }

        public int StateId { get; set; } //?
    }
}