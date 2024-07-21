using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities
{
    public class Product:BaseEntity
    {
        [Range(1,100000)]
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        // We are commenting this because we use it when SaveProduct which is in Product services to reduce the number of calls.
        public virtual Category Category { get; set; }
        //if we use virtual keyword then enitty framework also fetch the categroy record all details, thats why we are using virtual keyword.
        public string ImageURL { get; set; }
    }
}
