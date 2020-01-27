using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using FinanceManager.Models;
using FinanceManager.DAL;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    public class BudgetController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.IsNotDashboard = true;

            BudgetAdapter budgetAdapter = new BudgetAdapter();
            List<BudgetModel> AllUserBudgets = budgetAdapter.GetBudgetsByUID(Utilities.GetUsersUID(User.Identity.Name));

            //Can delete budgets
            //Can duplicate budgets

            //Can Add a Budget via Modal
            //Select what Budget to View via list

            //Selecting one bring up data for that budget

            //Assign budget to Account Types
            //Can exclude Transactions by
            //  : Category
            //  : Include Essentials
            //  : Exclude Essentials
            //Click 'Save' to save settings

            return View();
        }


        [HttpGet]
        public JsonResult GetBudgetByID(long id)
        {
            BudgetAdapter budgetAdapter = new BudgetAdapter();
            BudgetModel selectedBudget = budgetAdapter.GetBudgetByID(id);

            return Json(selectedBudget);
        }

        [HttpPost]
        public ActionResult UpdateBudget(BudgetModel budgetToUpdate)
        {
            BudgetAdapter budgetAdapter = new BudgetAdapter();
            budgetAdapter.SetBudget(budgetToUpdate, User.Identity.Name);

            //Refresh page
            //Re-get info
            ViewBag.PreviouslySelectedBudget = budgetToUpdate.ID;

            return RedirectToAction("Index", "Budget", null);
        }


        [HttpPost]
        public ActionResult AddBudget(BudgetModel budgetToAdd)
        {
            BudgetAdapter budgetAdapter = new BudgetAdapter();
            budgetAdapter.AddBudget(budgetToAdd, User.Identity.Name);

            return RedirectToAction("Index", "Budget", null);
        }

        public ActionResult DeleteBudget(long ID)
        {
            BudgetAdapter budgetAdapter = new BudgetAdapter();
            budgetAdapter.DeleteBudgetByIDAndUID(ID, User.Identity.Name);

            return RedirectToAction("Index", "Budget", null);
        }

    }
}