using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Website_Ecommerce.API.Data;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.ModelDtos;

namespace Website_Ecommerce.API.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DataContext _dataContext;
        public IUnitOfWork UnitOfWork => _dataContext;

        public IQueryable<Cart> Carts => _dataContext.Carts;

        public CartRepository(DataContext dataContext){
            _dataContext = dataContext;
        }

        public void Add(Cart item)
        {
            _dataContext.Carts.Add(item);
        }

        public void Delete(Cart item)
        {
            _dataContext.Entry(item).State  = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void Update(Cart item)
        {
            _dataContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task<IEnumerable<ViewItemCartDto>> GetAllItemByIdUser(int id)
        {
            var itemCart = _dataContext.Carts.Where(x => x.UserId == id && x.State == true);
            var pdetail = _dataContext.ProductDetails;
            var product = _dataContext.Products;
            var itemPdetail = itemCart.Join(pdetail, i => i.ProductDetailId, pd => pd.Id
                                        ,(i, pd) => new{
                                            id = i.Id,
                                            idProduct = pd.ProductId,
                                            idProductDetail = pd.Id,
                                            initialprice = pd.InitialPrice,
                                            price = pd.Price,
                                            amount = i.Amount
                                        });

            return await itemPdetail.Join(product, ip => ip.idProduct, p => p.Id, (ip, p) => new ViewItemCartDto{
                                            Id = ip.id,
                                            NameProduct = p.Name,
                                            IdProductDetail = ip.idProductDetail,
                                            UserId = id,
                                            InitialPrice = ip.initialprice,
                                            Price = ip.price,
                                            Amount =ip.amount
                                        }).ToListAsync();
        }
    }
}