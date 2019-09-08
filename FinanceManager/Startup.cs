using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinanceManager.Startup))]
namespace FinanceManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
