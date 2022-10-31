using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.services
{
    public interface IIdentityServices
    {
        string CreateToken(int userId, string username, List<string> roles);
    }
}