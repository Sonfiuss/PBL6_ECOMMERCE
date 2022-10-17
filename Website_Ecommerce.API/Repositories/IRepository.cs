using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Repositories
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}