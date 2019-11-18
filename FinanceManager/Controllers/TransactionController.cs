using FinanceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    public class TransactionController : Controller
    {
        TransactionsAdapter transactionsAdapter = new TransactionsAdapter();
        AccountTypeAdapter accountTypeAdapter = new AccountTypeAdapter();

        [Authorize]
        public ActionResult Index()
        {

            List<AccountTypeModel> types = accountTypeAdapter.GetAccountTypesByUID(Utilities.GetUsersUID(User.Identity.Name));

            //Gets transactions / available account types for the given user, put them in a model to display it on the view
            TransactionModelList transactions = new TransactionModelList
            {
                TransactionModels = transactionsAdapter.GetTransactionsByUID(Utilities.GetUsersUID(User.Identity.Name)),
                AvailableAccountTypes = accountTypeAdapter.GetAccountTypesByUID(Utilities.GetUsersUID(User.Identity.Name)).Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.AccountType })
            };

            ViewBag.IsNotDashboard = true;

            //Home page for transactions page
            return View(transactions);
        }

        [HttpPost]
        public ActionResult AddTransaction(TransactionModelList transaction)
        {
            transactionsAdapter.AddTransaction(transaction.AddNewTransactionModel, User.Identity.Name);

            return RedirectToAction("Index", "Transaction", null);
        }
        
        public ActionResult DeleteTransaction(long ID)
        {
            transactionsAdapter.DeleteTransactionByID(ID,User.Identity.Name);

            return RedirectToAction("Index","Transaction",null);
        }
    }
}