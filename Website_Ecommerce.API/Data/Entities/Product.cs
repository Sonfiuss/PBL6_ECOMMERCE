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
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Material { get; set; }
        public string Origin { get; set; }
        public int Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set;}

        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        public Shop Shop { get; set;}
        public int Status { get; set; }

        public ICollection<ProductDetail> ProductDetails { get; set; }
        public IList<OrderDetail> OrderDetails { get; set; }
        public ICollection<VoucherProduct> VoucherProducts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Cart> Carts { get; set; }
        

    }
}