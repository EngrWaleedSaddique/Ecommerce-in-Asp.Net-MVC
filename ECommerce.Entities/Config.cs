using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities
{
    public class Config
    {
        //We use Key data annotation to make the propert as Primary Key in database table.
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
