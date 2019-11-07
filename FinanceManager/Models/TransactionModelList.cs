using System.Collections.Generic;

namespace FinanceManager.Models
{
    public class TransactionModelList
    {
        public List<TransactionModel> TransactionModels { get; set; }

        public TransactionModel NewModel { get; set; }
    }
}
