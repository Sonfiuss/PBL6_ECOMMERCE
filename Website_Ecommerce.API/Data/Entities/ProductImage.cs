using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public bool MainImage { get; set; }
        public Product Product { get; set; }
    }
}