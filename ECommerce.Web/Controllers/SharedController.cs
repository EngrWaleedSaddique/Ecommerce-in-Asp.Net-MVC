using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Web.Controllers
{
    public class SharedController : Controller
    {
        public JsonResult UploadImage()
        {
            JsonResult result = new JsonResult();
            //By default MVC didnot allow us to send the JSON response in Get request we have to enable it by adding below line.
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var file = Request.Files[0];
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/content/images/"),fileName);
                file.SaveAs(path);
                result.Data = new { Success = true,ImageURL=string.Format("/content/images/{0}",fileName)};


            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message=ex.Message };
            }
            return result;

        }
    }
}