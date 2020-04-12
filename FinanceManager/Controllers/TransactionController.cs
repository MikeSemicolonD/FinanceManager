using System.Linq;
using System.Web.Mvc;
using FinanceManager.DAL;
using FinanceManager.Models;

namespace FinanceManager.Controllers
{
    public class TransactionController : Controller
    {
        TransactionsAdapter transactionsAdapter = new TransactionsAdapter();
        AccountTypeAdapter accountTypeAdapter = new AccountTypeAdapter();
        CategoryAdapter categoryAdapter = new CategoryAdapter();

        [Authorize]
        public ActionResult Index(TransactionModel transactionModelTryingToAdd = null)
        {

            //Gets transactions / available account types for the given user, put them in a model to display it on the view
            TransactionModelList transactions = new TransactionModelList
            {
                TransactionModels = transactionsAdapter.GetTransactionsByUID(Utilities.GetUsersUID(User.Identity.Name)),
                AvailableAccountTypes = accountTypeAdapter.GetAccountTypesByUID(Utilities.GetUsersUID(User.Identity.Name)).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.AccountType }),
                Categories = categoryAdapter.GetCategories().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Category })
            };

            if (transactionModelTryingToAdd != null)
                transactions.AddNewTransactionModel = transactionModelTryingToAdd;

            transactions.AddNewTransactionModel.AvailableAccountTypeList = transactions.AvailableAccountTypes;
            transactions.AddNewTransactionModel.CategoryList = transactions.Categories;


            ViewBag.IsNotDashboard = true;

            //Home page for transactions page
            return View(transactions);
        }

        [HttpPost]
        public ActionResult AddTransaction(TransactionModel transaction)
        {
            if(!ModelState.IsValid)
                return RedirectToAction("Index", "Transaction", transaction);


            transactionsAdapter.AddTransaction(transaction, User.Identity.Name);

            return RedirectToAction("Index", "Transaction", null);
        }
        
        public ActionResult DeleteTransaction(long ID)
        {
            transactionsAdapter.DeleteTransactionByID(ID,User.Identity.Name);

            return RedirectToAction("Index","Transaction",null);
        }
    }
}