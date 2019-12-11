using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using FinanceManager.Models;

namespace FinanceManager.DAL
{
    public class FrequencyAdapter
    {
        public FrequencyModel GetFrequencyByBudgetUID(string UID)
        {
            FrequencyModel frequency = new FrequencyModel();

            try
            {
                SqlDataProvider db = new SqlDataProvider();

                string query = "SELECT * FROM [dbo].[Frequency] Fr LEFT JOIN [dbo].[Budget] B ON Fr.ID WHERE Fr.ID = B.Frequency_ID WHERE B.ID = " + UID + ";";

                using (SqlConnection connection = (SqlConnection) db.GetConnection())
                {
                    connection.Open();

                    SqlCommand command = db.CreateCommand(query, connection);
                    SqlDataReader reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        frequency = new FrequencyModel()
                        {
                            ID = Utilities.ParseInt(reader["ID"].ToString()),
                            Frequency = reader["Frequency"].ToString()
                        };
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return frequency;
        }

        public List<FrequencyModel> GetAllFrequencies()
        {
            List<FrequencyModel> frequencies = new List<FrequencyModel>();

            try
            {
                SqlDataProvider db = new SqlDataProvider();

                string query = "SELECT * FROM [dbo].[Frequency];";

                using (SqlConnection connection = (SqlConnection) db.GetConnection())
                {
                    connection.Open();

                    SqlCommand command = db.CreateCommand(query, connection);
                    SqlDataReader reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        FrequencyModel frequency = new FrequencyModel()
                        {
                            ID = Utilities.ParseInt(reader["ID"].ToString()),
                            Frequency = reader["Frequency"].ToString()
                        };

                        frequencies.Add(frequency);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return frequencies;
        }
    }
}