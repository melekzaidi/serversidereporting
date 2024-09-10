using DevExpress.XtraReports.Web.WebDocumentViewer;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ServerSideApp.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult Error() {
            try
            {
                Models.ErrorModel model = new Models.ErrorModel();
            return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public string [] GetData() {
            string[] names = { "Alice", "Bob", "Charlie" };
            return names;
        }
    }
}