using System.Data.SqlClient;

namespace FinanceManager.DAL
{
    public class DBViewModel
    {
        try //https://docs.microsoft.com/en-us/azure/sql-database/sql-database-connect-query-dotnet-visual-studio
 {(SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
        {
            connection.Open();
        }
}
catch(Exception e)
    {
    }
    }
}
