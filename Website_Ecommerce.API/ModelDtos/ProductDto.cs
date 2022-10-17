using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.ModelDtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Material { get; set; }
        public string Origin { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int ShopId { get; set; }
        public int Status { get; set; }
    }
}