using ECommerce.Services;
using ECommerce.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        #region AccountController User Manager
        //Here we pasted the Account Controller User manager code to access the name of the user and pass it in view model of checkout.
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion
        // GET: Order
        public ActionResult Index(string userID,string status, int? pageNo)
        {
            var pageSize = ConfigurationService.Instance.PageSize();
            OrdersViewModel model = new OrdersViewModel();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.Orders = OrdersService.Instance.SearchOrders(userID, status,pageNo.Value,pageSize);
            model.UserID = userID;
            model.Status = status;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var totalRecords = OrdersService.Instance.SearchOrdersCount(userID,status);
            model.Pager = new Pager(totalRecords, pageNo, 10);
            return View(model);
        }

        public ActionResult Details(int ID)
        {
            var pageSize = ConfigurationService.Instance.PageSize();
            OrdersDetailViewModel model = new OrdersDetailViewModel();
            model.Order= OrdersService.Instance.GetOrderByID(ID);
            if(model.Order!=null)
            {
                model.OrderBy = UserManager.FindById(model.Order.UserID);
            }
            model.AvailableStatuses = new List<string>() { "Pending","In Progress","Delivered"};

            return View(model);
        }

        public JsonResult ChangeStatus(string status,int ID)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data=new { Success = OrdersService.Instance.UpdateOrderStatus(ID,status)};
            return result;
        }
    }
}
