using System;
using FinanceManager.DAL;
using System.Data.SqlClient;

public class UserAdapter
{ 
    public string GetUIDByEmail(string email)
    {
        string UID = "";

        try
        {
            string query = "SELECT A.Id FROM [dbo].[AspNetUsers] A WHERE A.Email = '" + email + "';";

            SqlDataProvider db = new SqlDataProvider();

            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();

                SqlCommand command = db.CreateCommand(query, connection);
                SqlDataReader reader = db.ExecuteReader(command);

                while (reader.Read())
                {
                    UID = reader["Id"].ToString();
                }

                reader.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return UID;
    }
}