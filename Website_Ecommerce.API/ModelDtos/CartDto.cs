using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.ModelDtos
{   
    public class CartDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductDetailId { get; set; }
        public int Amount { get; set; }
        
    }
    public class ViewItemCartDto{
        public int Id { get; set; }
        public int UserId { get; set; }
        public int IdProductDetail { get; set; }
        public string NameProduct { get; set;}
        public int IdProductDetail {get;set;}
        public double InitialPrice { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public int State{ get; set;}
    }
}