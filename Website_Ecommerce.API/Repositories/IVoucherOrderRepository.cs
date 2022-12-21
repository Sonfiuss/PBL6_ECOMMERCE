using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Repositories
{
    public interface IVoucherOrderRepository : IRepository<VoucherOrder>
    {
        void Add(VoucherOrder voucherOrder);
        void Update(VoucherOrder voucherOrder);
        IQueryable<VoucherOrder> VoucherOrders { get; }
        Task<IEnumerable<VoucherOrder>> GetAllVoucherbyDate(DateTime start, DateTime end);
        // Task<IList<VoucherOrder>> GetAllVoucherbyCheckVoucher(Order order);
        Task<IList<VoucherOrder>> GetAllVoucherMatch();
    }
}