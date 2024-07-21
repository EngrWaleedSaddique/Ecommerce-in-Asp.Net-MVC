
using ECommerce.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ECommerce.Entities;
using System.Dynamic;

namespace ECommerce.Services
{
    public class CategoriesService
    {
        #region Singleton
        public static CategoriesService Instance
        {
            get
            {
                if (instance == null) instance = new CategoriesService();
                return instance;
            }
        }
        private static CategoriesService instance { get; set; }
        private CategoriesService()
        {

        }
        #endregion

        public void SaveCategory(Category category)
        {
            using (var context= new CBContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }
        public void UpdateCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteCategory(int ID)
        {
            using (var context = new CBContext())
            {
                var category = context.Categories.Find(ID);
                //context.Entry(category).State = System.Data.Entity.EntityState.Deleted;


                /*Sometimes deltetion of Category give us a error of F.k constraint and didn,t delete the category.In such situation we have to
                delete the products of related category then we have to delete the category below is the code to delete the realated products.
                If we are fetching products with categories also then this didn,t give us error. But if we are not fetching products with categories then
                we use the below code to delete products of related category.
                Add this two lines of code and remove the above line var category also if f.k constraint give us error at the time of deletion.

                var category = context.Categories.Where(x=>x.ID==ID).Include(x=>x.Products).FirstOrDefault();
                context.Products.RemoveRange(category.Products);
                 */
                context.Categories.Remove(category);

                context.SaveChanges();
            }
        }
        public List<Category> GetCategories(string search,int pageNo)
        {
            int pageSize = 3;
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(c => c.Name.ToLower().Contains(search.ToLower()))
                    .OrderBy(x => x.ID)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Products)
                    .ToList();
                }
                else
                {
                    return context.Categories
                        .OrderBy(x => x.ID)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();
                }
            }
        }
        public List<Category> GetAllCategories()
        {
            using (var context = new CBContext())
            {
                return context.Categories.ToList();
            }
        }
        public int GetCategoriesCount(string search)
        {
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(c => c.Name.ToLower().Contains(search.ToLower())).Count();
                }
                else
                {
                    return context.Categories.Count();
                }

                
            }
        }
        public List<Category> GetFeaturedCategories()
        {
            using (var context = new CBContext())
            {

                return context.Categories.Where(x=>x.isFeatured&&x.ImageURL!=null).ToList();

            }
        }

        public Category GetCategory(int ID)
        {
            using (var context = new CBContext())
            {

                return context.Categories.Find(ID);

            }
        }
    }

}