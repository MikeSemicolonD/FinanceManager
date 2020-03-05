using System.Linq;
using System.Web.Mvc;
using FinanceManager.DAL;

namespace FinanceManager.Controllers
{
    public class HomeController : Controller
    {
        TransactionsAdapter transactionsAdapter = new TransactionsAdapter();
        AccountTypeAdapter accountTypeAdapter = new AccountTypeAdapter();
        CategoryAdapter categoryAdapter = new CategoryAdapter();
        BudgetAdapter budgetAdapter = new BudgetAdapter();
        FrequencyAdapter frequencyAdapter = new FrequencyAdapter();

        [Authorize]
        public ActionResult Index()
        {
            //Get Budgets
            //Get Transactions
            ViewBag.AccountTypes = accountTypeAdapter.GetAccountTypesByUID(Utilities.GetUsersUID(User.Identity.Name)).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.AccountType });
            var transactionList = transactionsAdapter.GetTransactionsByUID(Utilities.GetUsersUID(User.Identity.Name));
            ViewBag.Transactions = transactionList.GetRange(0,(transactionList.Count > 5) ? 5 : transactionList.Count);
            ViewBag.Categories = categoryAdapter.GetCategories().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Category });
            ViewBag.Budgets = budgetAdapter.GetBudgetsByUID(Utilities.GetUsersUID(User.Identity.Name));
            ViewBag.Frequencies = frequencyAdapter.GetAllFrequencies().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Frequency });
            //ViewBag.AccountTypes = accountTypeAdapter.GetAccountTypesByUID(Utilities.GetUsersUID(User.Identity.Name));

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
    }
}