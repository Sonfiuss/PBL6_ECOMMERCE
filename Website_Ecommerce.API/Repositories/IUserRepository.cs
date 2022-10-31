using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User> Users { get; }
        void Add(User user);
        void Update(User user);
        void Delete(User user);

        IQueryable<UserRole> UserRoles { get; }
        void Add(UserRole userRole);
        void Update(UserRole userRole);
        void Delete(UserRole userRole);

    }
}