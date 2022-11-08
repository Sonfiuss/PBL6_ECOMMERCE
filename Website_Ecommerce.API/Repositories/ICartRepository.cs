using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.ModelDtos;

namespace Website_Ecommerce.API.Repositories
{
    public interface ICartRepository:IRepository<Cart>
    {
        void Add(Cart item);
        void Update(Cart item);
        void Delete(Cart item);
        IQueryable<Cart> Carts { get; }
        Task<IEnumerable<ViewItemCartDto>> GetAllItemByIdUser(int id);
    }
}