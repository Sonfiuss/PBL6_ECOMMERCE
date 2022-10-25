using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }
<<<<<<< HEAD
        public String Name { get; set; }
        public String Config { get; set; }
        public String CreateDate { get; set; }
=======
        [Required]
        public String NameMethod { get; set; }
        [Required]
        public String Config { get; set; } /// JSon

        public ICollection<Payment> Payments { get; set; }
>>>>>>> 31f0e805f63357d227287102869eabfb0c22e234

    }
}