using ECommerce.Services;
using ECommerce.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ECommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        //CategoriesService categoryService = new CategoriesService();
        // GET: Home
        public ActionResult Index()
        {
            HomeViewModels model = new HomeViewModels();
            model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();
            return View(model);
        }
        public JsonResult LogOut()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            result.Data = new { Success = true};
            return result;
        }
    }
}