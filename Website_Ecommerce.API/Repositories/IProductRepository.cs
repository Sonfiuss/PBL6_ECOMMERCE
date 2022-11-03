using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> Products { get; }

        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        IQueryable<ProductDetail> ProductDetails { get; }
        void Add(ProductDetail productDetail);
        void Update(ProductDetail productDetail);
        void Delete(ProductDetail productDetail);
        IQueryable<ProductImage> ProductImages { get; }
        void Add(ProductImage productImage);
        void Update(ProductImage productImage);
        void Delete(ProductImage productImage);
        Product GetProductbyId(int productId);
    }
}