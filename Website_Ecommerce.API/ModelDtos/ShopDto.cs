using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.ModelDtos
{
    public class ShopDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Status { get; set; }
    }
}