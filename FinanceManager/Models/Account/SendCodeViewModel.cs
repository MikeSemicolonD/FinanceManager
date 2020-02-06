namespace FinanceManager.Models
{
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public System.Collections.Generic.ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
