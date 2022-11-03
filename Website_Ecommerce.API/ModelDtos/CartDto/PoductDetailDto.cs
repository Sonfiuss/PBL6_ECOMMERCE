using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semester7.PBL6.Website_Ecommerce.API.ModelDtos.CartDto
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Amount { get; set; }
        public int InitialPrice{ get; set;}
        public double Price { get; set; }

    }
}