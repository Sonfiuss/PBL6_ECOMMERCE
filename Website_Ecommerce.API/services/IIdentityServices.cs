using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Website_Ecommerce.API.services
{
    public interface IIdentityServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <param name="expires"></param>
        /// <returns></returns>
        string GenerateToken(int userId, string username, List<int> roles, int expires);
        string GetMD5(string text);
        bool VerifyMD5Hash(string inputHash, string hashVerify);
    }
}