using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.Repositories;

namespace Semester7.PBL6.Website_Ecommerce.API.Repositories
{
    public interface IProductDetailRepository : IRepository<ProductDetail>
    {
        void AddItem(ProductDetail item);
        void UpdateItem(ProductDetail item);
        void DeleteItem(ProductDetail item);
        Task<IEnumerable<ProductDetail>> GetAllItemByIdProduct(int id);
        Task<ProductDetail> GetItembyId(int id);


    }
}