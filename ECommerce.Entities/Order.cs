using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities
{
    public class Order
    {
        public int ID { get; set; }
        //We are declaring here UserID as string because In Authentication And Authorization User Manager is inherited from Identity.And in Identity
        // the data type of UserID is string type so thats we declare UserID as a string.
        public string UserID { get; set; }
        public DateTime OrderedAt { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }

    }
}
