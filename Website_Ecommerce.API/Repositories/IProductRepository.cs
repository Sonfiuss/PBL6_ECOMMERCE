using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product? GetProductById(int id);

        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int Id);
    }
}