using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class UserRole
    {
        [Key]
<<<<<<< HEAD
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public List<User> users { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public List<Role> roles { get; set; }
=======
        [Column(Order = 1)]
        public int UserId { get; set; }
        public User User { get; set; }

        [Key]
        [Column(Order = 2)]
        public int RoleId { get; set; }
        public Role Role { get; set; }
>>>>>>> 31f0e805f63357d227287102869eabfb0c22e234
    }
}