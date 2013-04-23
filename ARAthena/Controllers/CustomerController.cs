using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace athena.AR.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            return View();
        }

        //
        // GET: /Customer/Create
        public ActionResult Create()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            return View(new Models.CustomerModel()
                {
                     DateCreated = DateTime.Now
                });
        }

        //
        // Post: /Customer/Create
        [HttpPost]
        public ActionResult Create(Models.CustomerModel model)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            try
            {
                var customer = new athena.AR.Library.Customer(User.Identity.Name);
                customer.Create(model.Name, model.Address, model.Phone, model.Email, model.Active);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.ExceptionLogger.AppendAsync(ex);
                throw ex;
            }
        }


        public ActionResult List(string param = "All", int startIndex =0)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

              var totcount =0;
              var currCount = 0;
              var customer = new athena.AR.Library.Customer(User.Identity.Name);
              var customerList = customer.GetList(param, startIndex ,out  totcount ,out currCount);
               var customers = customerList.Select(c => new athena.AR.Models.CustomerModel
                   {
                       Id = c.Id, Name = c.Name, Address = c.Address, Phone = c.Phone, Email = c.Email, DateCreated = c.DateCreated, LastDateModified = c.LastDateModified, Active = c.Active
                   }).ToList();

            var prevIndex = startIndex - 20;
              if (prevIndex < 0)
                  prevIndex = 0;
              ViewBag.TotalCount = totcount;
              ViewBag.LastIndex = prevIndex;
              ViewBag.NextIndex = startIndex + currCount;
              ViewBag.RecordRange = string.Format("{0} - {1} of {2}", startIndex + 1, startIndex + currCount, totcount);
              ViewBag.PrevStat = prevIndex;
              ViewBag.NextStat = totcount == currCount ? 0 : 1;
              ViewBag.Param = param;
              return View(customers);
            }

        }
    }

