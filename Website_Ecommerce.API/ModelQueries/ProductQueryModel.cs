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
        // public int Saled { get; set; }
    }
}