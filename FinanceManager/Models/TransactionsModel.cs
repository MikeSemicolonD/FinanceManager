using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Models
{
    public class TransactionsModel
    {
        int ID { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        bool IsEssential { get; set; }

        string Category { get; set; }

        decimal Price { get; set; }

        int AccountID { get; set; }

        string AccountType { get; set; }


    }
}
