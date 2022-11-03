using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Semester7.PBL6.Website_Ecommerce.API.Repositories;
using Website_Ecommerce.API.Data;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.Repositories;

namespace Semester7.PBL6.Website_Ecommerce.API.Repositories
{
    public class ProductDetailRepository : IProductDetailRepository
    {
        private readonly DataContext _dataContext;
        public ProductDetailRepository(DataContext dataContext){
            _dataContext = dataContext;
        }

        public IUnitOfWork UnitOfWork => _dataContext;

        public void AddItem(ProductDetail item)
        {
            _dataContext.ProductDetails.Add(item);
        }

        public void DeleteItem(ProductDetail item)
        {   item.Amount = 0;
            _dataContext.ProductDetails.Update(item);
        }

        public async Task<ProductDetail> GetItembyId(int id)
        {
            return await _dataContext.ProductDetails
                .Where(x => x.Id == id)
                .FirstAsync();
        }

        public void UpdateItem(ProductDetail item)
        {
            _dataContext.ProductDetails.Update(item);
        }

        async Task<IEnumerable<ProductDetail>> IProductDetailRepository.GetAllItemByIdProduct(int id)
        {
            return await _dataContext.ProductDetails.Where(p => p.ProductId == id).ToListAsync();
        }
    }
}