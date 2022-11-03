using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Semester7.PBL6.Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.Repositories
{
    public interface ICategroyRepository : IRepository<Category>
    {
        IQueryable<Category> Categories { get; }
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}