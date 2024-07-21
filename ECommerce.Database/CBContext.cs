using ECommerce.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Database
{
    public class CBContext :DbContext,IDisposable
    {
        public CBContext():base("bazar")
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product>  Products{get;set;}
        public DbSet<Config> Configurations { get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
