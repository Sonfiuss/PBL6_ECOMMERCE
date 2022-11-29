using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.ModelDtos;

namespace Website_Ecommerce.API.services
{
    public interface IStatisticService
    {
        Task<IList<ShopStatisticProductDto>> StatisticProduct(int idShop);
    }
}