using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Repositories
{
    public interface IShopRepository : IRepository<Shop>
    {
        IQueryable<Shop> Shops { get; }
        void Add(Shop shop);
        void Update(Shop shop);
        void Delete(Shop shop);
        IQueryable<VoucherProduct> voucherProducts{get;}
        void Add(VoucherProduct voucherProduct);
        void Update(VoucherProduct voucherProduct);
        void Delete(VoucherProduct voucherProduct);
        IList<VoucherProduct> GetGetVoucherMatch(ProductDetail productdetail);
    }
}