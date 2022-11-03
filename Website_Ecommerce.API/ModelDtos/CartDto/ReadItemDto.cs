using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semester7.PBL6.Website_Ecommerce.API.ModelDtos.CartDto
{
    public class ReadItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InitialPrice { get; set; }
        public double Price { get; set; }
        public int Amount { get; set;}

        public ReadItemDto(int id, string name, int initialprice, double price, int amount){
            this.Id = id;
            this.Name = name;
            this.InitialPrice = initialprice;
            this.Price = price;
            this.Amount = amount;

        }

        public ReadItemDto()
        {
        }
    }
}