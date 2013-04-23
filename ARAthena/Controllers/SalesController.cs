using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using athena.AR.Models;
using athena.AR.Security;
using athena.AR.Library;

namespace athena.AR.Controllers
{
    public class SalesController : Controller
    {

        //private List<Models.AccountReceivableModel> _ARList = null;

        public SalesController()
        {
            
        }
        //
        // GET: /Sales/

        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn","Account");
            return View();
        }

        //
        // GET: /Sales/SalesData
        public ActionResult SalesData(int startIndex = 0)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            try
            {
                var sales = new athena.AR.Library.Sales(User.Identity.Name);
                 var totcount= 0;
                var currCount = 0;
                  
                var salesList  = sales.GetList(startIndex,out totcount ,out currCount);
                var salesData = salesList.Select(s => new SalesModel(s)).ToList();

                var prevIndex = startIndex - 20;
                   if (prevIndex < 0)
                       prevIndex = 0;
                   ViewBag.TotalCount  = totcount;
                   ViewBag.LastIndex = prevIndex;
                   ViewBag.NextIndex = startIndex+currCount;
                   ViewBag.RecordRange = string.Format("{0} - {1} of {2}", startIndex + 1, startIndex + currCount, totcount);
                   ViewBag.PrevStat = prevIndex;
                    ViewBag.NextStat = totcount == currCount ? 0 : 1;
                     return View(salesData.ToList());
                   
                }          


            catch (Exception ex)
            {
                Common.ExceptionLogger.AppendAsync(ex);
                throw ex;
            }
        }

        //
        // GET: /Sales/SalesHistory
        public ActionResult SalesHistory()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            return View();
        }

        //
        // GET: /Sales/Details/5

        public ActionResult Details(int id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            return View();
        }

        //
        // GET: /Sales/Create

        public ActionResult Create()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            var newSales = new Models.SalesModel() 
            {
                Qty = 1,  SalesDate = DateTime.Now , Customer = new CustomerModel() ,
                 MonthlyInterest    = 0,
                 MonthsToPay = 1
            };
            return View(newSales);
        } 

        //
        // POST: /Sales/Create

        [HttpPost]
        public ActionResult Create(Models.SalesModel model)
        {

            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            try
            {
                var sales = new Sales(User.Identity.Name);
                sales.Create(model.Customer.Id, model.Item, model.Qty, model.UnitPrice, model.SalesDate, model.MonthlyInterest, model.MonthsToPay);
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                Common.ExceptionLogger.AppendAsync(ex);
                throw ex;
            }
        }
        
        //
        // GET: /AccountReceivable/Edit/fwenrekdweROEreJROWFD45464mFML45LJFDMF
 
        public ActionResult Edit(string id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            return View();
        }

        //
        // POST: /AccountReceivable/Edit/fwenrekdweROEreJROWFD45464mFML45LJFDMF

        [HttpPost]
        public ActionResult Edit(string id, Models.ReceivableModel accountReceivable)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.ExceptionLogger.AppendAsync(ex);
                throw ex;
            }
        }

        //
        // GET: /AccountReceivable/Delete/5
 
        public ActionResult Delete(string id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            return View();
        }

        //
        // POST: /AccountReceivable/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

            

        [HttpGet]
        public ActionResult PaymentHistory(string Id)
        {
              if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

              try
              {
                  //var _arId = Security.EncryptionManager.DecryptStringId(Id);
                  //using (var dataContext = new Data.AREntities(ConnectionHelper.GetConnection()))
                  //{
                  //    var paymentHistoryQuery = from p in dataContext.PAYMENTs
                  //                              where (p.RECEIVABLEID == _arId)
                  //                              select new PaymentHistoryModel()
                  //                              {
                  //                                  AmountPaid = p.AMOUNT,
                  //                                  PaymentDate = (DateTime)p.PAYMENTDATE,
                  //                                  DateEncoded = p.LASTMODIFIEDDATE 
                  //                              };
                  //    var count = paymentHistoryQuery.Count();
                  //    return View(paymentHistoryQuery.ToList());
                  //}
                  return View();
                  
              }
              catch (Exception) { return RedirectToAction("Index");}
        }
    }
}
