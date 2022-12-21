using Website_Ecommerce.API.Data;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly DataContext _dataContext;

        public ShopRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IQueryable<Shop> Shops => _dataContext.Shops;

        public IUnitOfWork UnitOfWork => _dataContext;

        public IQueryable<VoucherProduct> voucherProducts => _dataContext.VoucherProducts;
        public void Add(Shop shop)
        {
            _dataContext.Shops.Add(shop);
        }

        public void Add(VoucherProduct voucherProduct)
        {
            _dataContext.VoucherProducts.Add(voucherProduct);
        }

        public void Delete(Shop shop)
        {
            _dataContext.Entry(shop).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void Delete(VoucherProduct voucherProduct)
        {
            _dataContext.Entry(voucherProduct).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public IList<VoucherProduct> GetGetVoucherMatch(ProductDetail productdetail)
        {
            return _dataContext.VoucherProducts.Where(p => p.MinPrice < productdetail.Price).ToList();
        }

        public void Update(Shop shop)
        {
            _dataContext.Entry(shop).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Update(VoucherProduct voucherProduct)
        {
            _dataContext.Entry(voucherProduct).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

    }
}