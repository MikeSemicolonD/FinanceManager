using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FinanceManager.Controllers
{
    public class AnalyticsController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Your Analytics page.";

            return View();
        }

    }
}