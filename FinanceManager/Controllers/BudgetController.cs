using System.Web.Mvc;
using FinanceManager.DAL;
using FinanceManager.Models;

namespace FinanceManager.Controllers
{
    public class BudgetController : Controller
    {

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.IsNotDashboard = true;

            BudgetAdapter budgetAdapter = new BudgetAdapter();
            FrequencyAdapter frequencyAdapter = new FrequencyAdapter();
            AccountTypeAdapter accountTypeAdapter = new AccountTypeAdapter();

            BudgetModelList pageData = new BudgetModelList
            {
                Budgets = budgetAdapter.GetBudgetsByUID(Utilities.GetUsersUID(User.Identity.Name)),
                Categories = budgetAdapter.GetUniqueCategoryByUID(Utilities.GetUsersUID(User.Identity.Name)),
                Frequencies = frequencyAdapter.GetAllFrequencies(),
                AccountTypes = accountTypeAdapter.GetAccountTypesByUID(Utilities.GetUsersUID(User.Identity.Name)),
                NewBudget = new BudgetModel()
            };


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
            return View(pageData);
        }


        [HttpGet]
        public JsonResult GetBudgetByID(long id)
        {
            BudgetAdapter budgetAdapter = new BudgetAdapter();
            BudgetModel selectedBudget = budgetAdapter.GetBudgetByID(id);

            return Json(selectedBudget, JsonRequestBehavior.AllowGet);
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