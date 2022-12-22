using Microsoft.EntityFrameworkCore;
using Website_Ecommerce.API.Data;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.ModelDtos;
using Website_Ecommerce.API.ModelQueries;

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

        public IQueryable<ProductCategory> ProductCategories => _dataContext.ProductCategories;

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

        public void Add(ProductCategory productCategory)
        {
            _dataContext.ProductCategories.Add(productCategory);
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

        public void Delete(ProductCategory productCategory)
        {
            _dataContext.Entry(productCategory).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

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

        public void Update(ProductCategory productCategory)
        {
            _dataContext.Entry(productCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        /// <summary>
        /// Avg Rating of product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<double> AvgRating(int id)
        {
            int[] listValueRate = await _dataContext.Comments.Where(x => x.Rate != 0).Select(x => x.Rate).ToArrayAsync();
            int totalRate = await _dataContext.Comments.CountAsync(x => x.ProductId == id);
            int sum = 0;
            foreach (int i in listValueRate)
            {
                sum += i;
            }
            double result = sum / totalRate;

            return Math.Round(result, 1);
        }

        /// <summary>
        /// Count rating of product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> CountRating(int id)
        {
            return await _dataContext.Comments.CountAsync(x => x.ProductId == id);
        }

        /// <summary>
        /// Get adll product
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ProductQueryModel>> GetAllProduct()
        {
            var products = _dataContext.Products;
            var productdetails = _dataContext.ProductDetails;
            var productimages = _dataContext.ProductImages;

            var pro_prodetail = products.Join(productdetails, p => p.Id, pd => pd.ProductId,
                                        (p, pd) => new
                                        {
                                            id = p.Id,
                                            name = p.Name,
                                            productdetailId = pd.Id,
                                            price = pd.Price,
                                            initialPrice = pd.InitialPrice
                                        });


            var result = await pro_prodetail.Join(productimages, ppd => ppd.productdetailId, img => img.ProductDetailId,
                                        (ppd, img) => new ProductQueryModel
                                        {
                                            Id = ppd.id,
                                            Name = ppd.name,
                                            InitialPrice = ppd.initialPrice,
                                            Price = ppd.price,
                                            ImageURL = img.UrlImage
                                        }).ToListAsync();
            var result2 = (from p in result
                           group p by new { p.Id } //or group by new {p.ID, p.Name, p.Whatever}
                   into mygroup
                           select mygroup.FirstOrDefault()).OrderByDescending(x => x.Id).ToList();

            // var data = await _dataContext.Products
            //     .Join(_dataContext.ProductDetails,
            //     product => product.Id,
            //     productDetail => productDetail.ProductId,
            //     (product, productDetail) => new { product, productDetail})
            //     .Join(_dataContext.ProductImages,
            //     productDetail => productDetail.,
            //     productimage => productimage.ProductDetailId,
            //     (productdetail, productimage) => new ProductQueryModel
            //     {
            //         Id = productdetail.
            //     })

            return result2;
        }
    }
}