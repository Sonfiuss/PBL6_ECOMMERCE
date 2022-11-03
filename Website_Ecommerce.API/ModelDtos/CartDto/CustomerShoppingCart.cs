using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Semester7.PBL6.Website_Ecommerce.API.ModelDtos.CartDtoame
{
    public class CustomerShoppingCart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductDetailId { get; set; }
        public int Amount { get; set; }

    }
}