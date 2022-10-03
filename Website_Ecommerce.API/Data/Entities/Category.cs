using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class Category
    {
        [Key]
        public int ID_Category{ get; set;}
        public String CategoryName{ get; set;}
        
    }
}