using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Repositories
{
    public interface IShopRepository
    {
        IQueryable<Shop> Shops { get; }
        void Add(Shop shop);
        void Update(Shop shop);
        void Delete(Shop shop);
    }
}