

using Microsoft.EntityFrameworkCore;
using Website_Ecommerce.API.Data;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _dataContext;

        public IUnitOfWork UnitOfWork => _dataContext;

        public IQueryable<Order> Orders => _dataContext.Orders;

        public IQueryable<OrderDetail> OrderDetails => _dataContext.OrderDetails;

        public OrderRepository(DataContext dataContext){
            _dataContext = dataContext;
        }

        public void Add(Order order)
        {
            _dataContext.Orders.Add(order);
        }

        public async Task<Order> GetLastOrder(int userId)
        {
            var Orders =  _dataContext.Orders;
            var orderDetails = _dataContext.OrderDetails;
            
            return await _dataContext.Orders.LastOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<IEnumerable<Order>> GetAllOrderOfUser(int id)
        {
            return await _dataContext.Orders.Where(x => x.UserId == id).ToListAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetail(int id)
        {
            return await _dataContext.OrderDetails.Where(x => x.OrderId == id).ToListAsync();
        }

        public void Delete(Order  order )
        {
            _dataContext.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void Add(OrderDetail orderDetail)
        {
            _dataContext.OrderDetails.Add(orderDetail);
        }

        public void Delete(OrderDetail orderDetail)
        {
            _dataContext.Entry(orderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

        }

        public void Update(OrderDetail orderDetail)
        {
            _dataContext.Entry(orderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}