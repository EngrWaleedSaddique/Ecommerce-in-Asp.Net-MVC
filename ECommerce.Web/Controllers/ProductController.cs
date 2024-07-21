using ECommerce.Entities;
using ECommerce.Services;
using ECommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        //ProductService productService = new ProductService();
        //CategoriesService categoriesService = new CategoriesService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductTable(string search,int? pageNo)
        {
            var pageSize = ConfigurationService.Instance.PageSize();
            ProductSearchViewModel model = new ProductSearchViewModel();
            model.SearchTerm = search;
            pageNo= pageNo.HasValue ?pageNo.Value>0?pageNo.Value : 1:1;
            var totalRecords = ProductService.Instance.GetProductsCount(search);
            model.Products = ProductService.Instance.GetProducts(search, pageNo.Value, pageSize);
            model.Pager = new Pager(totalRecords,pageNo,10);
            return PartialView(model);
        }


        public ActionResult Create()
        {
            var categories = CategoriesService.Instance.GetAllCategories();
            return PartialView(categories);
        }
        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        { 
                var newProduct = new Product();
                newProduct.Name = model.Name;
                newProduct.Description = model.Description;
                newProduct.Price = model.Price;
                newProduct.ImageURL = model.ImageURL;
                newProduct.Category = CategoriesService.Instance.GetCategory(model.CategoryID);
                //newProduct.CategoryID = model.CategoryID;
                //if we are doing large projects then we have to use above line that is commented.For this we have to add also one more attrbute in Product class
                // with name CategoryID and entity framework replace The existing cloumn name with this and make this as Foriegn key.We can use this to reduce the
                // number of database calls.
                ProductService.Instance.SaveProduct(newProduct);
                return RedirectToAction("ProductTable");
            
        }
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();
            var product = ProductService.Instance.GetProduct(ID);
            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryID = product.Category != null ? product.Category.ID : 0;
            model.AvailableCategories = CategoriesService.Instance.GetAllCategories();
            model.ImageURL = product.ImageURL;
            return PartialView(model);

        }
        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = new Product();
            existingProduct.ID = model.ID;
            existingProduct.CategoryID = model.CategoryID;
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;
            existingProduct.Category = CategoriesService.Instance.GetCategory(model.CategoryID);
            existingProduct.ImageURL = model.ImageURL;
            ProductService.Instance.UpdateProduct(existingProduct);
            return RedirectToAction("ProductTable");
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductService.Instance.DeleteProduct(ID);
            return RedirectToAction("ProductTable");
        }
        public ActionResult Details(int ID)
        {
            ProductViewModel model = new ProductViewModel();
            model.Product = ProductService.Instance.GetProduct(ID);
            return View(model);
        }
    }

}