using ECommerce.Entities;
using ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Web.ViewModels
{
    public class OrdersViewModel
    {
        public string UserID { get; set; }

        public List<Order> Orders { get; set; }
        public Pager Pager { get; set; }
        public string Status { get;set; }
    }
    public class OrdersDetailViewModel
    {
        public string UserID { get; set; }
        public Order Order { get; set; }
        public ApplicationUser OrderBy{ get; set; }
        public List<string> AvailableStatuses { get; set; }
    }
}