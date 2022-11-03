using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Semester7.PBL6.Website_Ecommerce.API.ModelDtos.CartDto;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.Repositories;

namespace Semester7.PBL6.Website_Ecommerce.API.Repositories
{
    public interface ICartRepository: IRepository<CartItem>
    {
        void AddItem(CartItem item);
        void RemoveItem(int id);
        void UpdateItem(CartItem item);
        CartItem GetItembyID(int id);
        void CaculatorDiscoutPrice();
        void ApplyVoucher(VoucherProduct voucher);
        Task<IEnumerable<ReadItemDto>> GetAllItemByIdUser(int id);
        IQueryable<CartItem> Items { get; }
    }
}