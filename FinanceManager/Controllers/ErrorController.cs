using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View("Error");
        }
    }
}