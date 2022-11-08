using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Website_Ecommerce.API.Data;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Repositories
{
    public class VoucherOrderRepository : IVoucherOrderRepository
    {
        private readonly DataContext _dataContext;
        public IUnitOfWork UnitOfWork => _dataContext;

        public IQueryable<VoucherOrder> VoucherOrders => _dataContext.VoucherOrders;

        public VoucherOrderRepository(DataContext dataContext){
            _dataContext = dataContext;
        }

        public void Add(VoucherOrder voucherOrder)
        {
            _dataContext.Add(voucherOrder);
        }

        public async Task<IEnumerable<VoucherOrder>> GetAllVoucherbyDate(DateTime start, DateTime end)
        {
            return await _dataContext.VoucherOrders.Where(x => x.CreateAt >= start && x.Expired <= end).ToListAsync();
        }

        public void Update(VoucherOrder voucherOrder)
        {
            _dataContext.Entry(voucherOrder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}