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
<<<<<<< HEAD
        public string Image { get; set; }
        public bool MainImage { get; set; }
=======
        
        [ForeignKey("ProductDetail")]
        public int ProductDetailId { get; set; }
        public ProductDetail ProductDetail { get; set; }
        [Required]
        public string UrlImage { get; set; }
>>>>>>> 31f0e805f63357d227287102869eabfb0c22e234
    }
}