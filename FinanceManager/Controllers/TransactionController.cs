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

            ViewBag.Transactions = transactionsAdapter.GetTransactionsByUID(Membership.GetUser().ProviderUserKey.ToString());
            
            //Home page for transactions page
            return View();
        }
    }
}