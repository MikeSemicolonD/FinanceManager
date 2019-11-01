﻿using System;
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
    }
}