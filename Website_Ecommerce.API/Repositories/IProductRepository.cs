using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.ModelQueries;

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

        IQueryable<ProductCategory> ProductCategories { get; }
        void Add(ProductCategory productCategory);
        void Update(ProductCategory productCategory);
        void Delete(ProductCategory productCategory);
        /// <summary>
        /// Get all product
        /// </summary>
        /// <returns></returns>
        Task<IList<ProductQueryModel>> GetAllProduct();

        /// <summary>
        /// Count Rating of Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> CountRating(int id);

        /// <summary>
        /// AvgRating of Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<double> AvgRating(int id);
    }
}