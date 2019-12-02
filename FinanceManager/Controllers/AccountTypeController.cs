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
        //[Authorize]
        //public ActionResult Index()
        //{
        //    //TransactionsAdapter transactionsAdapter = new TransactionsAdapter();

        //    //Gets transactions for the given user, put them in a Viewbag to display it on the view
        //    //ViewBag.Transactions = transactionsAdapter.GetTransactionsByUID(Utilities.GetUsersUID());
            
        //    //Home page for Account Type page
        //    return View();
        //}

        [HttpGet]
        public JsonResult GetUserAccountTypes()
        {
            AccountTypeAdapter accountTypeAdapter = new AccountTypeAdapter();

            List<AccountTypeModel> accountTypes = accountTypeAdapter.GetAccountTypesByUID(Utilities.GetUsersUID(User.Identity.Name));

            return Json(accountTypes.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveAccountType(long typeID)
        {
            AccountTypeAdapter accountTypeAdapter = new AccountTypeAdapter();

            accountTypeAdapter.DeleteAccountTypeByID(typeID);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddAccountType(string typeName)
        {
            AccountTypeAdapter accountTypeAdapter = new AccountTypeAdapter();

            accountTypeAdapter.AddAccountType(new AccountTypeModel { AccountType = typeName }, User.Identity.Name);

            return Json(true,JsonRequestBehavior.AllowGet);
        }
    }
}