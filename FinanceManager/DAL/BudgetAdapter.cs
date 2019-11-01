using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using FinanceManager.Models;
using FinanceManager.DAL;

public class BudgetAdapter
{
    /// <summary>
    /// Returns a list of budgets for a given user
    /// </summary>
    /// <param name="UID"></param>
    /// <returns></returns>
    public List<BudgetModel> GetBudgetsByUID(string UID)
    {
        string query = "SELECT * FROM [dbo].[Budget] b Where b.ID = '" + UID + "';";

        List<BudgetModel> budgets = new List<BudgetModel>();
        SqlDataProvider db = new SqlDataProvider();

        try
        {
            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();

                SqlCommand command = db.CreateCommand(query, connection);
                SqlDataReader reader = db.ExecuteReader(command);

                while (reader.Read())
                {
                    BudgetModel budget = new BudgetModel()
                    {
                        UID = reader["ID"].ToString(),
                        Account_ID = Utilities.ParseInt(reader["Account_ID"].ToString()),
                        Price = Utilities.ParseDecimal(reader["Price"].ToString()),
                        Times = Utilities.ParseInt(reader["Times"].ToString()),
                        Frequency_ID = Utilities.ParseInt(reader["Frequency_ID"].ToString()),
                    };

                    budgets.Add(budget);
                }

                reader.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return budgets;
    }

    public void DeleteBudget(BudgetModel budget)
    {
        //Parse each id into query
        string query = "DELETE * FROM [dbo].[Budgets] B WHERE B.ID = '"+ budget.UID+ "';";

        SqlDataProvider db = new SqlDataProvider();

        try
        {
            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();

                SqlCommand command = db.CreateCommand(query, connection);
                SqlDataReader reader = db.ExecuteReader(command);

                while (reader.Read())
                {
                    //TODO: Test this, Does deleting return a number or something?
                    var obj1 = reader[0];
                    var obj2 = reader[1];
                }

                reader.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public BudgetModel GetBudgetByUID(string UID)
    {
        string query = "SELECT * FROM [dbo].[Budgets] B WHERE B.ID = " + UID + ";";

        BudgetModel budget = new BudgetModel();
        SqlDataProvider db = new SqlDataProvider();

        try
        {
            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();

                SqlCommand command = db.CreateCommand(query, connection);
                SqlDataReader reader = db.ExecuteReader(command);

                while (reader.Read())
                {
                    budget = new BudgetModel()
                    {
                        UID = reader["ID"].ToString(),
                        Account_ID = Utilities.ParseInt(reader["Account_ID"].ToString()),
                        Price = Utilities.ParseDecimal(reader["Price"].ToString()),
                        Times = Utilities.ParseInt(reader["Times"].ToString()),
                        Frequency_ID = Utilities.ParseInt(reader["Frequency_ID"].ToString()),
                    };
                }

                reader.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return budget;
    }

    public void SetBudget(BudgetModel budget, string UserEmail)
    {
        SetBudgets(new List<BudgetModel>() { budget }, UserEmail);
    }

    public void SetBudgets(List<BudgetModel> Budgets, string UserEmail)
    {
        string queryTemplate = "UPDATE [dbo].[Budget] t SET Account_ID = '{0}', Price = '{1}', Times = {2}, Frequency_ID = {3} WHERE t.ID = {4}; ";
        string query = "";

        foreach (BudgetModel budget in Budgets)
        {
            //Account_ID, Price, Times, Frequency_ID, UID
            query += string.Format(queryTemplate, budget.Account_ID, budget.Price, budget.Times, budget.Frequency_ID, Utilities.GetUsersUID(UserEmail));
        }

        SqlDataProvider db = new SqlDataProvider();

        try
        {
            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();

                SqlCommand command = db.CreateCommand(query, connection);

                command.ExecuteScalar();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void AddBudget(BudgetModel budget, string UserEmail)
    {
        AddBudgets(new List<BudgetModel>() { budget }, UserEmail);
    }

    public void AddBudgets(List<BudgetModel> budgets, string UserEmail)
    {
        string queryTemplate = "INSERT INTO [dbo].[Budget] VALUES ('{0}',{1},{2},{3},{4});";
        string query = "";

        foreach (BudgetModel budget in budgets)
        {
            //Name, Description, IsEssential, Category, Price, Account_ID, AccountType
            query += string.Format(queryTemplate, (budget.UID.Length == 0) ? Utilities.GetUsersUID(UserEmail) : budget.UID, budget.Account_ID, budget.Price, budget.Times, budget.Frequency_ID);
        }

        SqlDataProvider db = new SqlDataProvider();

        try
        {
            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();

                SqlCommand command = db.CreateCommand(query, connection);

                command.ExecuteScalar();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}