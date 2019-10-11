using FinanceManager.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FinanceManager.DAL
{
    public class ApplicationContext
    {
        public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
        {
            public ApplicationDbContext()
                : base("DefaultConnection", throwIfV1Schema: false)
            {
            }

            public static ApplicationDbContext Create()
            {
                return new ApplicationDbContext();
            }
        }
    }
}