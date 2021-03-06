﻿using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Models
{
    public class BudgetModel
    {
        public int ID { get; set; }

        public string UID { get; set; }

        public int Account_ID { get; set; }

        public decimal Amount { get; set; }

        [Required]
        public string Description { get; set; }

        public int Category_ID { get; set; }

        public string Category { get; set; }

        public int Frequency_ID { get; set; }
    }
}
