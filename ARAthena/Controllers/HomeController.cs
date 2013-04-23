using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace athena.AR.Controllers
{
    public class HomeController : Controller
    {
        private List<string> _metroColors;

        public ActionResult Index()
        {
           if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            var ranNum = new Random();
            var functions = new List<Models.FunctionModel>();
          
            functions.Add(new Models.FunctionModel() { 
                Description1 = "Payments",
                Description2 = "The easiest way to record payments",
                Action = "/Payment/Index",
                Float = "left",
                Color = MetroColors[ranNum.Next(0, 9)],
                Controller = "Payment",
            });
            functions.Add(new Models.FunctionModel() { 
                Description1 = "Sales", 
                Description2 = "Contains all sales transaction.",
                Action = "/Sales/Index",
                Float = "left",
                Color = MetroColors[ranNum.Next(0, 9)],
                Controller = "Sales"
            });
            functions.Add(new Models.FunctionModel() { 
                Description1 = "Receivables",
                Description2 = "Use Accounts Receivables to records your business transactions.",
                Action = "/Receivable/Index",
                Float="left",
                Color = MetroColors[ranNum.Next(0, 9)],
                Controller = "Receivable"
            });
            functions.Add(new Models.FunctionModel()
            {
                Description1 = "Customers",
                Description2 = "Use Accounts Receivables to records your business transactions.",
                Action = "/Customer/Index",
                Color = MetroColors[ranNum.Next(0, 9)],
                Controller = "Customer"
            });
          
            return View(functions);
        }

               
        public ActionResult About()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            return View();
        }

        public ActionResult getView(string view)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            return PartialView(view);
        }

        #region Private Properties

        private List<string> MetroColors {
            get {
                if (_metroColors == null)
                {
                    _metroColors = new List<string>
                        {
                            "#A200FF",
                            "#FF0097",
                            "#00ABA9",
                            "#8CBF26",
                            "#A05000",
                            "#E671B8",
                            "#F09609",
                            "#1BA1E2",
                            "#E51400",
                            "#339933"
                        };
                }
                return _metroColors ; }
        }
        #endregion //Private Properties
    }
}

