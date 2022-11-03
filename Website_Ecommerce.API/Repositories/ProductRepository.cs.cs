using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public IQueryable<Product> Products => _dataContext.Products;

        public IUnitOfWork UnitOfWork => _dataContext;

        public IQueryable<ProductDetail> ProductDetails => _dataContext.ProductDetails;

        public IQueryable<ProductImage> ProductImages => _dataContext.ProductImages;

        public void Add(Product product)
        {
            _dataContext.Products.Add(product);
        }

        public void Add(ProductDetail productDetail)
        {
            _dataContext.ProductDetails.Add(productDetail);
        }

        public void Add(ProductImage productImage)
        {
            _dataContext.ProductImages.Add(productImage);
        }

        public void Delete(Product product)
        {
            _dataContext.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void Delete(ProductDetail productDetail)
        {
            _dataContext.Entry(productDetail).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void Delete(ProductImage productImage)
        {
            _dataContext.Entry(productImage).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public Product GetProductbyId(int productId)
        {
            return _dataContext.Products.Where(p => p.Id == productId).FirstOrDefault();
        }

        public void Update(Product product)
        {
            _dataContext.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Update(ProductDetail productDetail)
        {
            _dataContext.Entry(productDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Update(ProductImage productImage)
        {
            _dataContext.Entry(productImage).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}