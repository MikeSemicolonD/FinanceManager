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
            //Gets transactions for the given user, put them in a Viewbag to display it on the view
            //ViewBag.Transactions = transactionsAdapter.GetTransactionsByUID(Utilities.GetUsersUID());
            //string test = Utilities.GetUsersUID(User.Identity.Name).ToString();

            ViewBag.IsNotDashboard = true;
            ViewBag.Message = "You can put stuff into the viewBag and use it in the view later.";

            //Home page for transactions page
            return View();
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