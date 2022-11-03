using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Semester7.PBL6.Website_Ecommerce.API.ModelDtos.CartDto;
using Website_Ecommerce.API.Data;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.Repositories;

namespace Semester7.PBL6.Website_Ecommerce.API.Repositories
{   


    public class ShoppingCart : ICartRepository
    {
        private readonly DataContext _dataContext;
        public ShoppingCart(DataContext dataContext){
            _dataContext = dataContext;
        }
        public IUnitOfWork UnitOfWork => _dataContext;

        public IQueryable<CartItem> Items => _dataContext.Carts;

        public void AddItem(CartItem item)
        {
            _dataContext.Carts.Add(item);
        }

        public void ApplyVoucher(VoucherProduct voucher)
        {
            throw new NotImplementedException();
        }

        public void CaculatorDiscoutPrice()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReadItemDto>> GetAllItemByIdUser(int id)
        {
            var itemCart = _dataContext.Carts.Where(x => x.UserId == id);
            var pdetail = _dataContext.ProductDetails;
            var product = _dataContext.Products;
            var itemPdetail = itemCart.Join(pdetail, i => i.ProductDetailId, pd => pd.Id
                                        ,(i, pd) => new{
                                            id = i.Id,
                                            idProduct = pd.ProductId,
                                            initialprice = pd.InitialPrice,
                                            price = pd.Price,
                                            amount = i.Amount
                                        });

            return await itemPdetail.Join(product, ip => ip.idProduct, p => p.Id, (ip, p) => new ReadItemDto{
                                            Id = ip.id,
                                            Name = p.Name,
                                            InitialPrice = ip.initialprice,
                                            Price = ip.price,
                                            Amount =ip.amount
                                        }).ToListAsync();
        }

        public CartItem GetItembyID(int id)
        {
            return _dataContext.Carts.FirstOrDefault(p => p.Id == id);
        }

        public void RemoveItem(int id)
        {
            var item = GetItembyID(id);
            _dataContext.Carts.Remove(item);
        }

        public void UpdateItem(CartItem item)
        {
            _dataContext.Carts.Update(item);
        }

    }
}