using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IQueryable<User> Users => _dataContext.Users;

        public IQueryable<UserRole> UserRoles => _dataContext.UserRoles;

        public IUnitOfWork UnitOfWork => _dataContext;

        public void Add(User user)
        {
            _dataContext.Users.Add(user);
        }

        public void Add(UserRole userRole)
        {
            _dataContext.UserRoles.Add(userRole);
        }

        public void Delete(User user)
        {
            _dataContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void Delete(UserRole userRole)
        {
            _dataContext.Entry(userRole).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void Update(User user)
        {
            _dataContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Update(UserRole userRole)
        {
            _dataContext.Entry(userRole).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}