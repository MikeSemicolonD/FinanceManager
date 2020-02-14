using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FinanceManager
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            CultureInfo culture = new CultureInfo(Request.UserLanguages.FirstOrDefault());

            //en-US for some reasons shows negative dolloar sums as "($100)" instead of "-$100". 
            //(Guess we're too special to be using negative numbers)
            if (culture.Name == "en-US")
            {
                culture.NumberFormat.CurrencyNegativePattern = 1;
            }

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
