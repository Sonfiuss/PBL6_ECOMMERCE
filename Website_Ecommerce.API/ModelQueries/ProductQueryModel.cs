using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.ModelQueries
{
    public class ProductQueryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double InitialPrice { get; set; }
        public string ImageURL { get; set; }
        public int Saled { get; set; }
    }
    public class ProductImageQueryModel
    {
        public int Id { get; set; }
        public string UrlImage { get; set; }
    }
    public class ProductDetailQueryModel
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public double InitialPrice { get; set; }
        public List<ProductImageQueryModel> ProductImages { get; set; }
    }

    public class ProductProductDetailQueryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalRate { get; set; }
        public float AverageRate { get; set; }
        public List<ProductDetailQueryModel> ProductDetails { get; set; }
    }
}