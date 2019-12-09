using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using FinanceManager.Models;
using FinanceManager.DAL;

public class CategoryAdapter
{
    /// <summary>
    /// Returns a list of AccountTypes for a given user
    /// </summary>
    /// <param name="UID"></param>
    /// <returns></returns>
    public List<CategoryModel> GetCategories()
    {
        List<CategoryModel> Categories = new List<CategoryModel>();

        try
        {
            SqlDataProvider db = new SqlDataProvider();

            string query = "SELECT * from Category";

            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();

                SqlCommand command = db.CreateCommand(query, connection);
                SqlDataReader reader = db.ExecuteReader(command);

                while (reader.Read())
                {
                    CategoryModel Category = new CategoryModel()
                    {
                        ID = Utilities.ParseInt(reader["ID"].ToString()),
                        Category = reader["Category"].ToString()
                    };

                    Categories.Add(Category);
                }

                reader.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return Categories;
    }
    
}