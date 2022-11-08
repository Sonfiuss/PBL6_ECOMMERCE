using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Repositories
{
    public interface IVoucherOrderRepository:IRepository<VoucherOrder>
    {
        void Add(VoucherOrder voucherOrder);
        void Update(VoucherOrder voucherOrder);
        IQueryable<VoucherOrder> VoucherOrders{ get;} 
        Task<IEnumerable<VoucherOrder>> GetAllVoucherbyDate(DateTime start, DateTime end);

    }
}