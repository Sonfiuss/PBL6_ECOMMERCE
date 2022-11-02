using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public void Add(Shop shop)
        {
            _dataContext.Shops.Add(shop);
        }

        public void Delete(Shop shop)
        {
            _dataContext.Entry(shop).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void Update(Shop shop)
        {
            _dataContext.Entry(shop).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}