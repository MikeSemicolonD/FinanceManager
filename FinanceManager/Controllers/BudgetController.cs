using System.Collections.Generic;
using System.Web.Mvc;
using FinanceManager.DAL;
using FinanceManager.Models;
using Newtonsoft.Json;

namespace FinanceManager.Controllers
{
    public class BudgetController : Controller
    {

        [Authorize]
        public ActionResult Index(BudgetModel BudgetTryingToAdd = null)
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
                AccountTypes = accountTypeAdapter.GetAccountTypesByUID(Utilities.GetUsersUID(User.Identity.Name))
            };

            if (BudgetTryingToAdd != null)
                pageData.NewBudget = BudgetTryingToAdd;

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

        [HttpGet]
        public JsonResult UpdateBudget(string data)
        {
            Dictionary<string,string> variables = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            BudgetAdapter budgetAdapter = new BudgetAdapter();
            BudgetModel budget = budgetAdapter.GetBudgetByID(long.Parse(variables["Id"]));

            budget.Description = variables["Description"].ToString();
            budget.Amount = decimal.Parse(variables["Amount"]);
            budget.Frequency_ID = int.Parse(variables["FrequencyID"]);
            budget.Account_ID = int.Parse(variables["AccountTypeID"]);

            budgetAdapter.SetBudget(budget, User.Identity.Name);

            //Refresh page
            //Re-get info
            //ViewBag.PreviouslySelectedBudget = budgetToUpdate.ID;

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AddBudget(BudgetModel budgetToAdd)
        {
            if(!ModelState.IsValid)
                return RedirectToAction("Index", "Budget", budgetToAdd);


            BudgetAdapter budgetAdapter = new BudgetAdapter();
            budgetAdapter.AddBudget(budgetToAdd, User.Identity.Name);

            return RedirectToAction("Index", "Budget", null);
        }

        [HttpGet]
        public ActionResult DeleteBudget(long ID)
        {
            BudgetAdapter budgetAdapter = new BudgetAdapter();
            budgetAdapter.DeleteBudgetByIDAndUID(ID, User.Identity.Name);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}