using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Analytics()
        {
            ViewBag.Message = "Your analytics page.";

            return View();
        }
        public ActionResult Budget()
        {
            ViewBag.Message = "Your Budget page.";

            return View();
        }
        public ActionResult Transaction()
        {
            ViewBag.Message = "Your Transaction page.";

            return View();
        }
    }
}