using System;
using System.Data.SqlClient;
using System.Configuration;
using FinanceManager.Models;
using System.Data;

namespace FinanceManager.DAL
{
    public class TransactionsAdapter
    {
        public TransactionsModel GetTransactionByUID(string UID)
        {
            string query = "SELECT * FROM [dbo].[Transaction] t left join [dbo].[User_Transactions] ut on t.ID = ut.UID Where ut.UID = "+UID+";";
            using (SqlConnection dp = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, dp);
                    dp.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                           // reader.get

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                              //  output += reader[i] + " ";
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

                return new TransactionsModel { }
        }
    }
}