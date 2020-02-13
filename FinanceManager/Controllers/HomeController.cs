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

        [Authorize]
        public ActionResult Index()
        {
            //Get Budgets
            //Get Transactions
            ViewBag.AccountTypes = accountTypeAdapter.GetAccountTypesByUID(Utilities.GetUsersUID(User.Identity.Name)).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.AccountType });
            ViewBag.Transactions = transactionsAdapter.GetTransactionsByUID(Utilities.GetUsersUID(User.Identity.Name)).GetRange(0,5);
            ViewBag.Categories = categoryAdapter.GetCategories().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Category });
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