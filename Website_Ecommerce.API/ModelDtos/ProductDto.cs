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

    public class ProductImageDto
    {
        public int ProductDetailId { get; set; }
        public string UrlImage { get; set; }
    }

    public class ProductDetailDto
    {
        public int? Id { get; set; }
        public int ProductId { get; set; }

        public string Size { get; set; }
        public string Color { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public double InitialPrice { get; set; }
    }
}