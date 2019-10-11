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
            
            string query = "USE UserInfo GO SELECT name, database_id, create_dateFROM sys.databases; GO";
            using (SqlConnection dp = new SqlConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(query,dp);

                    dp.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(String.Format("{0}, {1}",
                                reader[0], reader[1]));
                        }
                    }

                    dp.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dp.Close();
                }
            }
        }
    }
}