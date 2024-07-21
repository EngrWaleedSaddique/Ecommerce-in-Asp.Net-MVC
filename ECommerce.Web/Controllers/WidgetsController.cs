using ECommerce.Services;
using ECommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool isLatestProducts,int? CategoryID=0)
        { 
            ProductsWidgetViewModel model = new ProductsWidgetViewModel();
            model.isLatestProducts = isLatestProducts;
            model.isRelatedProducts = false;
            if (isLatestProducts)
            {
                //Here we are showing latest products only
                model.Products = ProductService.Instance.GetLatestProducts(4);
            }
            else
            if(CategoryID.HasValue && CategoryID.Value>0)
            {
                model.Products= ProductService.Instance.GetProductsByCategory(CategoryID.Value,4);
                model.isRelatedProducts = true;
            }
            else
            {
                //here we show Products of all Category but not All Products.
               model.Products = ProductService.Instance.GetProducts(1,8);

            }
            return PartialView(model);
        }
    }
}
