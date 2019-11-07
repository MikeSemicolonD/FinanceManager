using FinanceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FinanceManager.Controllers
{
    public class TransactionController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            TransactionsAdapter transactionsAdapter = new TransactionsAdapter();
            //Gets transactions for the given user, put them in a model to display it on the view
            TransactionModelList transactions = new TransactionModelList
            {
                TransactionModels = transactionsAdapter.GetTransactionsByUID(Utilities.GetUsersUID(User.Identity.Name))
            };

            //string test = Utilities.GetUsersUID(User.Identity.Name).ToString();

            ViewBag.IsNotDashboard = true;

            //Home page for transactions page
            return View(transactions);
        }

        [HttpPost]
        public ActionResult AddTransaction(TransactionModel transaction)
        {
            TransactionsAdapter transactionsAdapter = new TransactionsAdapter();

            transactionsAdapter.AddTransaction(transaction, User.Identity.Name);

            return Index();
        }
    }
}