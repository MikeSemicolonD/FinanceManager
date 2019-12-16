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

        [HttpPost]
        public ActionResult AddBudget(BudgetModel budgetToAdd)
        {
            BudgetAdapter budgetAdapter = new BudgetAdapter();
            budgetAdapter.AddBudget(budgetToAdd, User.Identity.Name);

            return RedirectToAction("Index", "Transaction", null);
        }

        public ActionResult DeleteBudget(long ID)
        {
            BudgetAdapter budgetAdapter = new BudgetAdapter();
            //budgetAdapter.DeleteBudget(ID, User.Identity.Name);

            return RedirectToAction("Index", "Transaction", null);
        }

    }
}