using FinanceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FinanceManager.Controllers
{
    public class AccountTypeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            //TransactionsAdapter transactionsAdapter = new TransactionsAdapter();

            //Gets transactions for the given user, put them in a Viewbag to display it on the view
            //ViewBag.Transactions = transactionsAdapter.GetTransactionsByUID(Utilities.GetUsersUID());
            
            //Home page for transactions page
            return View();
        }

        [HttpPost]
        public ActionResult AddAccountType(string typeName)
        {
            bool SuccessfullyAdded = false;

            AccountTypeAdapter accountTypeAdapter = new AccountTypeAdapter();
            List<AccountTypeModel> types = accountTypeAdapter.GetAccountTypesByUID(User.Identity.Name);

            bool duplicate = false;

            foreach (AccountTypeModel accounts in types)
            {
                if (accounts.AccountType == typeName)
                {
                    duplicate = true;
                }
            }

            if (!duplicate)
            {
                accountTypeAdapter.AddAccountType(new AccountTypeModel { AccountType = typeName }, User.Identity.Name);
                SuccessfullyAdded = true;
            }

            return Json(SuccessfullyAdded,JsonRequestBehavior.AllowGet);
        }
    }
}