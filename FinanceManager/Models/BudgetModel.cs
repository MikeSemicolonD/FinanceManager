namespace FinanceManager.Models
{
    public class BudgetModel
    {
        public string UID { get; set; }

        public int Account_ID { get; set; }

        public decimal Price { get; set; }

        //Number of times a budget is alloted per frequency (Ex: 3 times Per/Month, 5 times Per/Week)
        public int Times { get; set; }

        public int Frequency_ID { get; set; }
    }
}
