namespace FinanceManager.Models
{
    public class TransactionModel
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public bool IsEssential { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public int AccountID { get; set; }

        public string AccountType { get; set; }

    }
}
