using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
<<<<<<< HEAD
        public string Name { get; set; }
=======
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        public IList<UserRole> UserRoles { get; set; }

>>>>>>> 31f0e805f63357d227287102869eabfb0c22e234
    }
}