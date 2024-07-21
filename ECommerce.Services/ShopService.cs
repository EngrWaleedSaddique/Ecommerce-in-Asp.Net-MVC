using ECommerce.Database;
using ECommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class ShopService
    {
        #region Singleton
        public static ShopService Instance
        {
            get
            {
                if (instance == null) instance = new ShopService();
                return instance;
            }
        }
        private static ShopService instance { get; set; }
        private ShopService()
        {

        }
        #endregion

        public int SaveOrder(Order order)
        {
            using (var context = new CBContext())
            {
                //if we not use the below line of code EntityState.Unchanged then the category is inserted again and in dropdown list the categories appear twice or more
                // time that we add product and Select category.To Overcome this issue we used EntityState.Unchanged to not add a new category in category table 
                // just add the category in product table.
                context.Orders.Add(order);
                return context.SaveChanges();
            }
        }

    }
}
