using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using athena.AR.Models;
using athena.AR.Security;

namespace athena.AR.Controllers
{
    public class ReceivableController : Controller
    {
        //
        // GET: /Receivable/

        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");

            return View();
        }

        public ActionResult Receivables(int param, int startIndex=0)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            try
            {
                var totcount =0;
                var currCount =0;

                var receivable  = new athena.AR.Library.Receivable();
                var receivableList = receivable.GetList(param, startIndex, out totcount, out currCount); //Fix02262013: replace currCount to totcount.
                var receivablesData = receivableList.Select(r => new ReceivableModel(r)).ToList();

                var prevIndex = startIndex - 20;
                if (prevIndex < 0)
                    prevIndex = 0;
                ViewBag.TotalCount = totcount;
                ViewBag.LastIndex = prevIndex;
                ViewBag.NextIndex = startIndex + currCount;
                ViewBag.Param = param;
                ViewBag.RecordRange = string.Format("{0} - {1} of {2}", startIndex + 1, startIndex + currCount, totcount);
                ViewBag.PrevStat = prevIndex;
                ViewBag.NextStat = totcount == currCount ? 0 : 1;
                return View(receivablesData.ToList());

            }
            catch (Exception ex)
            {
                Common.ExceptionLogger.AppendAsync(ex);
                return null;
            }
        }
    }
}
