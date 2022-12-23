using Microsoft.EntityFrameworkCore;
using Website_Ecommerce.API.Data;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.ModelQueries;

namespace Website_Ecommerce.API.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DataContext _dataContext;
        public IUnitOfWork UnitOfWork => _dataContext;

        public IQueryable<Cart> Carts => _dataContext.Carts;

        public CartRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(Cart item)
        {
            _dataContext.Carts.Add(item);
        }

        public void Delete(Cart item)
        {
            _dataContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void Update(Cart item)
        {
            _dataContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        /// <summary>
        /// Get all item by userId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ItemCartQueryModel>> GetAllItemByIdUser(int id)
        {
            var data = await _dataContext.Carts.Where(x => x.UserId == id && x.State == true)
                                        .Join(_dataContext.ProductDetails,
                                        cart => cart.ProductDetailId,
                                        productDetail => productDetail.Id,
                                        (cart, productDetail) => new { cart, productDetail })
                                        .Join(_dataContext.Products,
                                        cartProductDetail => cartProductDetail.productDetail.ProductId,
                                        product => product.Id,
                                        (cartProductDetail, product) => new ItemCartQueryModel
                                        {
                                            Id = cartProductDetail.cart.Id,
                                            NameProduct = product.Name,
                                            IdProductDetail = cartProductDetail.productDetail.Id,
                                            IdShop = product.ShopId,
                                            UserId = cartProductDetail.cart.UserId,
                                            InitialPrice = cartProductDetail.productDetail.InitialPrice,
                                            Price = cartProductDetail.productDetail.Price,
                                            Amount = cartProductDetail.productDetail.Amount
                                        })
                                        .ToListAsync();
            return data;

        }
    }
}