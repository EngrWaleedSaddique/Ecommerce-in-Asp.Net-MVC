using ECommerce.Entities;
using ECommerce.Services;
using ECommerce.Web.Models;
using ECommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        //CategoriesService categoryService = new CategoriesService();
        //ProductService productService = new ProductService();
        public ActionResult Index()
        {
            var categories=CategoriesService.Instance.GetAllCategories();
            return View(categories);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                CategoriesService.Instance.SaveCategory(category);
                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }

        public ActionResult CategoryTable(string search,int? pageNo)
        {

            CategorySearchViewModel model = new CategorySearchViewModel();
            pageNo= pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SearchTerm = search;
            var totalRecords = CategoriesService.Instance.GetCategoriesCount(search);
            model.Categories= CategoriesService.Instance.GetCategories(search,pageNo.Value);
            // here we are checking that Categories is null or not.If it is null then below logic in which we execute Categories.Where command Give us 
            // error.
            if (model.Categories != null)
            {
                model.Pager = new Pager(totalRecords, pageNo,3);
                
                //In industry this _ With View name is used when We hava a partial view then we have to use underscore with View Name.
                //But using underscore we need to "_CategoryTable",model then we have to send the model.Because mvc by default search for
                //CategoryTable not _CategoryTable so we have to add it expicitly.
                return PartialView("_CategoryTable", model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var category=CategoriesService.Instance.GetCategory(ID);
            return PartialView(category);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            CategoriesService.Instance.UpdateCategory(category);
            return RedirectToAction("CategoryTable");
        }

        //[HttpGet]
        //public ActionResult Delete(int ID)
        //{
        //    var category = categoryService.GetCategory(ID);
        //    return View(category);
        //}
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            CategoriesService.Instance.DeleteCategory(ID);
            return RedirectToAction("CategoryTable");
        }
    }
}
