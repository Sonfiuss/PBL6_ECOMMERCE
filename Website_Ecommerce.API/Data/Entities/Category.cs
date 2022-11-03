using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Website_Ecommerce.API.Data.Entities;

namespace Semester7.PBL6.Website_Ecommerce.API.Data.Entities
{
    public class Category
    {
         
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}