using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FinanceManager.Models
{
    public class TransactionModel
    {
        public int ID { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsEssential { get; set; }

        [Required]
        public int Category { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int AccountType { get; set; }

        public IEnumerable<SelectListItem> AvailableAccountTypeList { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }

    }
}
