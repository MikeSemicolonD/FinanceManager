using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FinanceManager.Controllers
{
    public class BudgetController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.IsNotDashboard = true;

            //Can Add a Budget via Modal
            //Select what Budget to View via dropdown
            //Click 'Edit'
                //Assign budget to Account Types
                //Can exclude Transactions by
                //  : Category
                //  : Include Essentials
                //  : Exclude Essentials
                //Click 'Save' to save settings

            return View();
        }

    }
}