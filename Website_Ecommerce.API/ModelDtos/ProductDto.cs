using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.ModelDtos
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Material { get; set; }
        public string Origin { get; set; }
        public string Description { get; set; }
        public int ShopId { get; set; }
        public int Status { get; set; }

        public HashSet<int> Categories { get; set; }
    }
}