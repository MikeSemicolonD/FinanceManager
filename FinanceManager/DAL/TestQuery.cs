using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace FinanceManager.DAL
{
    public class TestQuery
    {
        public void TestSelect()
        {
        //https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand?view=netframework-4.8
        //    SqlConnection dp = new SqlConnection();
        //    string query = "USE UserInfo GO SELECT name, database_id, create_dateFROM sys.databases; GO";
        //    using (DbConnection conn = dp.GetConnection())
        //    {
        //        try
        //        {
        //            SqlCommand command = dp.CreateCommand();
        //            conn.Open();
        //            cmd.ExecuteNonQuery();
        //            conn.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            if (conn.State == ConnectionState.Open)
        //            {
        //                conn.Close();
        //            }
        //        }
        //    }
        }
    }
}