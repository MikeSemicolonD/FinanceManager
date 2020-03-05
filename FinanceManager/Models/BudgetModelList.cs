using System.Collections.Generic;

namespace FinanceManager.Models
{
    public class BudgetModelList
    {
        public List<BudgetModel> Budgets { get; set; }

        public List<CategoryModel> Categories { get; set; }

        public List<AccountTypeModel> AccountTypes { get; set; }

        public List<FrequencyModel> Frequencies { get; set; }

        public BudgetModel NewBudget { get; set; } = new BudgetModel();

        public BudgetModel EdittingBudget { get; set; } = new BudgetModel();

    }
}