using System;
using System.Data.SqlClient;
using System.Configuration;

namespace FinanceManager.DAL
{
    public class TestQuery
    {
        public string TestSelect()
        {
            //https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand?view=netframework-4.8


            string output = "";
            string query = "SELECT * FROM sys.databases;";
            using (SqlConnection dp = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query,dp);
                    dp.Open();
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for(int i = 0; i < reader.FieldCount; i++)
                            {
                                output += reader[i] + " ";
                            }

                            //output += reader[i++] + " ";

                            // Console.WriteLine(String.Format("{0}, {1}", reader[0], reader[1]));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dp.Close();
                }
                    return output;
            }
        }
    }
}