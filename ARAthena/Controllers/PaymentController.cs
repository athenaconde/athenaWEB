using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using athena.AR.Security;
using athena.AR.Models;

namespace athena.AR.Controllers
{
    public class PaymentController : Controller
    {
        //
        // GET: /Payment/

        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            return View();
        }

        //
        // GET: /Payment/Submit/sSDskjfdFeo324i33#idF>fdfjr3434o343u4

        public ActionResult Submit(string Id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            try
            {
                    var payment = new athena.AR.Library.Payment(User.Identity.Name);
                    var newPayment = payment.New(Id);

                    var newPaymentEntry = new Models.PaymentModel(newPayment);

                    return View(newPaymentEntry);
           }
            catch (Exception ex) { 
                Common.ExceptionLogger.AppendAsync(ex);
                throw ex;
            }


        }

        //
        // POST: /Payment/Submit/sSDskjfdFeo324i33#idF>fdfjr3434o343u4
        [HttpPost]
        public ActionResult Submit(string Id, Models.PaymentModel model)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            try
            {
                var payment = new athena.AR.Library.Payment(User.Identity.Name);
                payment.CreatePayment(model.AccountReceivable.Id, model.PayAmount, model.PaymentDate);
                return RedirectToAction("Index");
            }
            catch (Exception ex) { 
                Common.ExceptionLogger.AppendAsync(ex);
                throw ex;
            }

        }

        //
        // GET: /Payment/PaymentHistory
        [HttpGet]
        public ActionResult PaymentHistory(int startIndex = 0)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            try
            {
                    var payment = new athena.AR.Library.Payment(User.Identity.Name);
                    
                    var totcount =0;
                    var currCount = 0;

                    var paymentsData = payment.GetPaymentHistory(startIndex, out totcount, out currCount);
                    var paymentsHistory = paymentsData.Select(p => new Models.PaymentModel(p)
                        {
                            PayAmount = p.PayAmount, PaymentDate = p.PaymentDate, EncodedDate = p.EncodedDate
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
                    return View(paymentsHistory);
             
            }


            catch (Exception ex)
            {
                Common.ExceptionLogger.AppendAsync(ex);
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult ExtendedActions(PaymentModel payment)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            return View();
        }
    }
}
