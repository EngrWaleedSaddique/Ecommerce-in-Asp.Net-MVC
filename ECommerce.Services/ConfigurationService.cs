using ECommerce.Database;
using ECommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class ConfigurationService
    {
        #region Singleton
        public static ConfigurationService Instance
        {
            get
            {
                if (instance == null) instance = new ConfigurationService();
                return instance;
            }
        }
        private static ConfigurationService instance { get; set; }
        private ConfigurationService()
        {

        }
        #endregion

        public Config GetConfig(string Key)
        {
            using (var context=new CBContext())
            {
                return context.Configurations.Find(Key);
            }
        }
        public int PageSize()
        {
            using (var context = new CBContext())
            {
                var pageSizeConfig = context.Configurations.Find("PageSize");
                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 10;
            }
            
       }
        public int ShopPageSize()
        {
            using (var context = new CBContext())
            {
                var pageSizeConfig = context.Configurations.Find("ShopPageSize");
                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
            }

        }
    }

}