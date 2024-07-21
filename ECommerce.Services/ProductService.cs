
using ECommerce.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ECommerce.Entities;

namespace ECommerce.Services
{
    public class ProductService
    {
        #region Singleton
        public static ProductService Instance
        {
            get
            {
                if (instance == null) instance = new ProductService();
                return instance;
            }
        }
        private static ProductService instance { get; set; }

        private ProductService()
        {

        }
        #endregion


        public int GetMaximumPrice()
        {
            using (var context = new CBContext())
            {
                return (int)(context.Products.Max(x=>x.Price));

            }
        }
        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy,int pageNo,int pageSize)
        {
            using (var context = new CBContext())
            {
                var products = context.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x=>x.Category.ID==categoryID.Value).ToList();
                }

                if(!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x=>x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    products = products.Where(x=>x.Price>=minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maximumPrice.Value).ToList();
                }

                if(sortBy.HasValue)
                {
                    switch(sortBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        case 4:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                    }
                }


                return products.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int SearchProductsCount(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
        {
            using (var context = new CBContext())
            {
                var products = context.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.Category.ID == categoryID.Value).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maximumPrice.Value).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        case 4:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                    }
                }


                return products.Count;
            }
        }
        public void SaveProduct(Product product)
        {
            using (var context= new CBContext())
            {
                //if we not use the below line of code EntityState.Unchanged then the category is inserted again and in dropdown list the categories appear twice or more
                // time that we add product and Select category.To Overcome this issue we used EntityState.Unchanged to not add a new category in category table 
                // just add the category in product table.

                context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;
                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        public void UpdateProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteProduct(int ID)
        {
            using (var context = new CBContext())
            {
                //context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                var product = context.Products.Find(ID);
                context.Products.Remove(product);

                context.SaveChanges();
            }
        }
        public List<Product> GetProducts(int pageNo)
        {
            //When we intialize the Category as virtual in entities. Entity Framework will genrate the query but didn,t execute it.It is executed when we access
            // the value of Category. Example in Product Table we are using @product.Category.Name now the query is called and if we use the below code
            // then the object is disposed it give an error thats we are writing it outside the using statement and comment the section of using code.

            //using (var context = new CBContext())
            //{
            //    return context.Products.ToList();
            //}

            // (Note if we use the below code the connection is not disposed, which create performance issues.If we do this it is correct but we comment it now and use
            // proper approach for it which is used in industry now.)

            //var context = new CBContext();
            //return context.Products.ToList();


            int pageSize = 10;
            using (var context = new CBContext())
            {
                return context.Products.OrderBy(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
                //return context.Products.Include(x => x.Category).ToList();
            }

            //We inherit our CBContext from Idisposeable class because it we have to dispose the database object because of performance issues it is necassary,
            // when  there are many user connected with your application.
        }
        public List<Product> GetProducts(string search, int pageNo, int pageSize)
        {
            
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(product => product.Name!=null&&product.Name.ToLower().Contains(search.ToLower()))
                    .OrderBy(x => x.ID)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Category)
                    .ToList();
                }
                else
                {
                    return context.Products
                        .OrderBy(x => x.ID)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Category)
                        .ToList();
                }
            }
        }
        public int GetProductsCount(string search)
        {

            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(product => product.Name != null && product.Name.ToLower().Contains(search.ToLower()))
                    .OrderBy(x => x.ID).Count();
                }
                else
                {
                    return context.Products
                        .OrderBy(x => x.ID).Count();
                }
            }
        }

        public List<Product> GetProducts(int pageNo,int pageSize)
        {
            using (var context = new CBContext())
            {
                return context.Products.OrderByDescending(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetLatestProducts(int numberOfProducts)
        {
            using (var context = new CBContext())
            {
                return context.Products.OrderByDescending(x => x.ID).Take(numberOfProducts).Include(x => x.Category).ToList();
            }
        }
        public List<Product> GetProductsByCategory(int categoryID,int pageSize)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(x=>x.Category.ID==categoryID).OrderByDescending(x => x.ID).Take(pageSize).Include(x => x.Category).ToList();
            }
        }
        

        public Product GetProduct(int ID)
        {
            using (var context = new CBContext())
            {

                //return context.Products.Find(ID);
                //If we want the record of category of related product then we write the below linq.
                return context.Products.Where(product => product.ID == ID).Include(product => product.Category).FirstOrDefault();

            }
        }

        public List<Product> GetProducts(List<int> IDs)
        {
            using (var context = new CBContext())
            {

                return context.Products.Where(product => IDs.Contains(product.ID)).ToList();

            }
        }
    }

}
