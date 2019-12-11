using FinanceManager.DAL;
using FinanceManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    public class AccountTypeController : Controller
    {
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