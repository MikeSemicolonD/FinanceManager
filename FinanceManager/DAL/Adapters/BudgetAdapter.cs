using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using FinanceManager.Models;

namespace FinanceManager.DAL
{
    public class BudgetAdapter
    {
        /// <summary>
        /// Returns a list of budgets belonging to a given user
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public List<BudgetModel> GetBudgetsByUID(string UID)
        {
            List<BudgetModel> budgets = new List<BudgetModel>();

            try
            {
                SqlDataProvider db = new SqlDataProvider();

                string query = "SELECT * FROM [dbo].[Budget] as b left join [dbo].[User_Budget] as ub on b.ID = ub.Budget_ID Where ub.UID = '" + UID + "';";

                using (SqlConnection connection = (SqlConnection) db.GetConnection())
                {
                    connection.Open();

                    SqlCommand command = db.CreateCommand(query, connection);
                    SqlDataReader reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        BudgetModel budget = new BudgetModel()
                        {
                            ID = Utilities.ParseInt(reader["ID"].ToString()),
                            Category_ID = Utilities.ParseInt(reader["Category_ID"].ToString()),
                            Description = reader["Description"].ToString(),
                            Account_ID = Utilities.ParseInt(reader["Account_ID"].ToString()),
                            Amount = Utilities.ParseDecimal(reader["Amount"].ToString()),
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

        /// <summary>
        /// Returns a list of unique categories in budgets belonging to a given user
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public List<CategoryModel> GetUniqueCategoryByUID(string UID)
        {
            List<CategoryModel> Categories = new List<CategoryModel>();

            try
            {
                SqlDataProvider db = new SqlDataProvider();

                string query = "SELECT distinct Category_ID, c.Category FROM [dbo].[Budget] as b left join [dbo].[User_Budget] as ub on b.ID = ub.Budget_ID left join [dbo].[Category] as c on b.Category_ID = c.ID Where ub.UID = '" + UID + "';";

                using (SqlConnection connection = (SqlConnection)db.GetConnection())
                {
                    connection.Open();

                    SqlCommand command = db.CreateCommand(query, connection);
                    SqlDataReader reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        CategoryModel category = new CategoryModel()
                        {
                            ID = Utilities.ParseInt(reader["Category_ID"].ToString()),
                            Category = reader["Category"].ToString()
                        };

                        Categories.Add(category);
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

        /// <summary>
        /// Deletes a Budget that belongs to specified user
        /// </summary>
        /// <param name="budget"></param>
        public void DeleteBudgetByIDAndUID(long id, string UID)
        {
            try
            {
                SqlDataProvider db = new SqlDataProvider();

                //Parse each id into query
                string query = "DELETE FROM [dbo].[User_Budget] AS UB WHERE UB.UID = '" + UID + "' AND UB.Budget_ID = " + id + "; DELETE FROM [dbo].[Budget] AS b WHERE b.ID = " + id;

                using (SqlConnection connection = (SqlConnection)db.GetConnection())
                {
                    connection.Open();

                    SqlCommand command = db.CreateCommand(query, connection);
                    SqlDataReader reader = db.ExecuteReader(command);

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Deletes a Budget that belongs to specified user, taking in a model rather than an ID
        /// </summary>
        /// <param name="budget"></param>
        public void DeleteBudget(BudgetModel budget)
        {
            try
            {
                SqlDataProvider db = new SqlDataProvider();

                //Parse each id into query
                string query = "DELETE FROM [dbo].[User_Budget] AS UB WHERE UB.UID = '" + budget.UID + "' AND UB.Budget_ID = " + budget.ID + "; DELETE FROM [dbo].[Budget] AS b WHERE b.ID = "+budget.ID;

                using (SqlConnection connection = (SqlConnection)db.GetConnection())
                {
                    connection.Open();

                    SqlCommand command = db.CreateCommand(query, connection);
                    SqlDataReader reader = db.ExecuteReader(command);

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Returns a single budget model belonging to a given user
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public BudgetModel GetBudgetByID(long ID)
        {

            BudgetModel budget = new BudgetModel();

            try
            {
                SqlDataProvider db = new SqlDataProvider();

                string query = "SELECT * FROM [dbo].[Budget] B WHERE B.ID = " + ID + ";";

                using (SqlConnection connection = (SqlConnection) db.GetConnection())
                {
                    connection.Open();

                    SqlCommand command = db.CreateCommand(query, connection);
                    SqlDataReader reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        budget = new BudgetModel()
                        {
                            ID = Utilities.ParseInt(reader["ID"].ToString()),
                            Category_ID = Utilities.ParseInt(reader["Category_ID"].ToString()),
                            Description = reader["Description"].ToString(),
                            Account_ID = Utilities.ParseInt(reader["Account_ID"].ToString()),
                            Amount = Utilities.ParseDecimal(reader["Amount"].ToString()),
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

        /// <summary>
        /// Updates the values of a single budget belonging to a given user
        /// </summary>
        /// <param name="budget"></param>
        /// <param name="UserEmail"></param>
        public void SetBudget(BudgetModel budget, string UserEmail)
        {
            SetBudgets(new List<BudgetModel>() { budget }, UserEmail);
        }

        /// <summary>
        /// Updates a list of budgets belonging to a given user
        /// </summary>
        /// <param name="Budgets"></param>
        /// <param name="UserEmail"></param>
        public void SetBudgets(List<BudgetModel> Budgets, string UserEmail)
        {
            try
            {
                SqlDataProvider db = new SqlDataProvider();

                //TODO: Update to support assigning categories
                string queryTemplate = "UPDATE [dbo].[Budget] SET Description = '{0}', Account_ID = '{1}', Amount = '{2}', Frequency_ID = {3} WHERE ID = {4}; ";
                string query = "";

                foreach (BudgetModel budget in Budgets)
                {
                    //Account_ID, Amount, Times, Frequency_ID, UID
                    query += string.Format(queryTemplate, budget.Description, budget.Account_ID, budget.Amount, budget.Frequency_ID, budget.ID);// (budget.UID.Length != 0) ? budget.UID : Utilities.GetUsersUID(UserEmail));
                }

                using (SqlConnection connection = (SqlConnection) db.GetConnection())
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

        /// <summary>
        /// Adds a single budget belonging to a given user
        /// </summary>
        /// <param name="budget"></param>
        /// <param name="UserEmail"></param>
        public void AddBudget(BudgetModel budget, string UserEmail)
        {
            AddBudgets(new List<BudgetModel>() { budget }, UserEmail);
        }

        /// <summary>
        /// Adds a list of budgets belonging to a given user. Adding to the [Budget] and the [User_Budget] table.
        /// </summary>
        /// <param name="budgets"></param>
        /// <param name="UserEmail"></param>
        public void AddBudgets(List<BudgetModel> budgets, string UserEmail)
        {
            try
            {
                string queryTemplate = "INSERT INTO [dbo].[Budget] VALUES ({0},'{1}',{2},{3},{4});";
                string query = "";

                List<int> NewBudgetIDs = new List<int>();

                foreach (BudgetModel budget in budgets)
                {
                    //Category_ID, Description, Account_ID, Amount, Frequency_ID
                    query += string.Format(queryTemplate, budget.Category_ID, budget.Description, budget.Account_ID, budget.Amount, budget.Frequency_ID);
                }

                //Returns the ID of the budgets we just created
                query += " SELECT SCOPE_IDENTITY() AS [NewIDs];";

                SqlDataProvider db = new SqlDataProvider();

                using (SqlConnection connection = (SqlConnection) db.GetConnection())
                {
                    connection.Open();

                    SqlCommand command = db.CreateCommand(query, connection);

                    SqlDataReader reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        NewBudgetIDs.Add(Utilities.ParseInt(reader["NewIDs"].ToString()));
                    }

                    reader.Close();

                    string template = "INSERT INTO [dbo].[User_Budget] VALUES('{0}',{1}); ";

                    string UTQuery = "";

                    string UserUID = Utilities.GetUsersUID(UserEmail);

                    foreach (int id in NewBudgetIDs)
                    {
                        UTQuery += string.Format(template, UserUID, id);
                    }

                    SqlCommand UTcommand = db.CreateCommand(UTQuery, connection);

                    UTcommand.ExecuteScalar();

                    reader.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}