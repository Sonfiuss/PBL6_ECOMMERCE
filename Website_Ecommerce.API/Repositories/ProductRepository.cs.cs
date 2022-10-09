using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Website_Ecommerce.API.Data;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dataContext;

        public ProductRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Product? GetProductById(int id)
        {
            return _dataContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProducts()
        {
            return _dataContext
            .Products
            .Include(p => p.Category) //join table category
            .ToList();
        }

        public void CreateProduct(Product product)
        {
            _dataContext.Products.Add(product);
            _dataContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var existedProduct = GetProductById(product.Id);
            if(existedProduct == null) return;
            existedProduct.Name = product.Name;
            existedProduct.CategoryId = product.CategoryId;
            existedProduct.ShopId = product.ShopId;
            existedProduct.Material = product.Material;
            existedProduct.Origin = product.Origin;
            existedProduct.Description = product.Description;
            existedProduct.Status = product.Status;
            _dataContext.Products.Update(existedProduct);
            _dataContext.SaveChanges();
        }
        

        public void DeleteProduct(int Id)
        {
            var existedProduct = GetProductById(Id);
            if(existedProduct == null) return;
            _dataContext.Products.Remove(existedProduct);
            _dataContext.SaveChanges();
        }
    }
}