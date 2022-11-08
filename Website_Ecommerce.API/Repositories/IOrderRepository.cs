using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        void CreateOrder(Order order);
        
    }
}