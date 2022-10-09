using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class ProductDetail
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string Size { get; set; }
        public string Color { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }

    }
}