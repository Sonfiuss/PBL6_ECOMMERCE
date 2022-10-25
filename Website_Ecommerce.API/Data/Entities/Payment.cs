using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
<<<<<<< HEAD
        public Order order { get; set; }
=======
        public Order Order { get; set; }
>>>>>>> 31f0e805f63357d227287102869eabfb0c22e234


        [ForeignKey("PaymentMethod")]
        public int PaymentMethodId { get; set; }
<<<<<<< HEAD
        public virtual PaymentMethod PayMethod { get; set; }

        public String Details { get; set; }
=======
        public PaymentMethod PaymentMethod { get; set; }
>>>>>>> 31f0e805f63357d227287102869eabfb0c22e234
    }
}