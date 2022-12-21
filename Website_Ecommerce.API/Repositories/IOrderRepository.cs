using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Add(Order order);
        void Delete(Order order);
        void Add(OrderDetail orderDetail);
        void Update(OrderDetail orderDetail);
        void Delete(OrderDetail orderDetail);
        IQueryable<Order> Orders { get; }
        IQueryable<OrderDetail> OrderDetails { get; }
        /// <summary>
        /// <param name = "userId"></param>
        /// </summary>
        Task<Order> GetLastOrder(int id);
        /// <summary>
        /// <param name = "userId"></param>
        /// </summary>
        Task<IEnumerable<Order>> GetAllOrderOfUser(int id);
        /// <summary>
        /// <param name = "orderId"></param>
        /// </summary>
        Task<IEnumerable<OrderDetail>> GetOrderDetail(int id);

    }
}