using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Models
{
    public class BudgetModel
    {
        string UID { get; set; }
        
        int AccountID { get; set; }

        decimal Price { get; set; }

        //Number of times a budget is alloted per frequency
        int Times { get; set; }

        int FrequencyID { get; set; }

        //Ex: PerMonth, PerDay, PerWeek, PerYear
        string Frequency { get; set; }
    }
}
