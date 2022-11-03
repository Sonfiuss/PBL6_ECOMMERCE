using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data;
using Website_Ecommerce.API.Data.Entities;

namespace Semester7.PBL6.Website_Ecommerce.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _dataContext;
        public OrderRepository(DataContext dataContext){
            _dataContext = dataContext;
        }
        public void Add(Order order)
        {
            _dataContext.Orders.Add(order);
        }

        public void Add(OrderDetail orderDetail)
        {
            _dataContext.OrderDetails.Add(orderDetail);
        }

        public void Update(Order order)
        {
            _dataContext.Orders.Update(order);
        }

        public void Update(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }
    }
}