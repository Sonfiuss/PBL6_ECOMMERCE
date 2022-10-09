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
        public String NameMethod { get; set; }
        public String Config { get; set; } /// JSon

        public ICollection<Payment> Payments { get; set; }

    }
}