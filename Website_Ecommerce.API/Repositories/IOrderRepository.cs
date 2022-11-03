using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Semester7.PBL6.Website_Ecommerce.API.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void Update(Order order);
        void Add(OrderDetail orderDetail);
        void Update(OrderDetail orderDetail);
    }
}