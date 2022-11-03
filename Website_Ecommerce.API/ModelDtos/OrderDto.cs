using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Semester7.PBL6.Website_Ecommerce.API.ModelDtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int State { get; set; }
        public string Address { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime SendDate { get; set; }
        public int UserId { get; set; }
        public int VoucherId { get; set; } 
        public Shipper Shipper { get; set; }
        public Payment Payment { get; set; }
        public IList<OrderDetail> OrderDetails { get; set; }
    }
}