using System.Web.Mvc;
using System.Collections.Generic;

namespace FinanceManager.Models
{
    public class TransactionModelList
    {
        public List<TransactionModel> TransactionModels { get; set; }
        
        public IEnumerable<SelectListItem> AvailableAccountTypes { get; set; }

        public TransactionModel AddNewTransactionModel { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
