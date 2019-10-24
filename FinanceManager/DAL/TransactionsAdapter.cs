using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using FinanceManager.Models;

namespace FinanceManager.DAL
{
    public class TransactionsAdapter
    {
        /// <summary>
        /// Returns a list of transactions for a given user
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public List<TransactionModel> GetTransactionsByUID(string UID)
        {
            string query = "SELECT * FROM [dbo].[Transaction] t left join [dbo].[User_Transactions] ut on t.ID = ut.UID Where ut.UID = " + UID + ";";

            List<TransactionModel> transactions = new List<TransactionModel>();
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
                        TransactionModel transaction = new TransactionModel()
                        {
                            //TODO: Finish this
                            ID = Utilities.ParseInt(reader["ID"].ToString()),
                            Category = reader["Category"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = Utilities.ParseDecimal(reader["Amount"].ToString()),
                            AccountID = Utilities.ParseInt(reader["Account_ID"].ToString()),
                            Date = Utilities.ParseDateTime(reader["Date"].ToString()),
                            IsEssential = Utilities.ParseBool(reader["IsEssential"].ToString())
                        };

                        transactions.Add(transaction);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return transactions;
        }

        public void DeleteTransactions(List<TransactionModel> transactions)
        {
            //Parse each id into query

            string template = "DELETE * FROM [dbo].[Transaction] t WHERE t.ID IN ({0});";
            string IDs = "";

            for(int i = 0; i < transactions.Count; i++)
            {
                IDs += transactions[i].ID.ToString();

                if(i != transactions.Count-1)
                {
                    IDs += ',';
                }
            }

            string query = string.Format(template,IDs);

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
                        //TODO: Does deleting return a number or something?
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

        public void DeleteTransactionByID(long ID)
        {
            string query = "DELETE * FROM [dbo].[Transaction] t WHERE t.ID = " + ID + ";";

            TransactionModel transaction = new TransactionModel();
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
                        //TODO: Does deleting return a number or something?
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

        public TransactionModel GetTransactionByID(long ID)
        {
            string query = "SELECT * FROM [dbo].[Transaction] t WHERE t.ID = " + ID + ";";

            TransactionModel transaction = new TransactionModel();
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
                        transaction = new TransactionModel()
                        {
                            ID = Utilities.ParseInt(reader["ID"].ToString()),
                            Category = reader["Category"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = Utilities.ParseDecimal(reader["Amount"].ToString()),
                            AccountID = Utilities.ParseInt(reader["Account_ID"].ToString()),
                            Date = Utilities.ParseDateTime(reader["Date"].ToString()),
                            IsEssential = Utilities.ParseBool(reader["IsEssential"].ToString())
                        };
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return transaction;
        }

        public void SetTransaction(TransactionModel transaction)
        {
            SetTransactions(new List<TransactionModel>() { transaction });
        }

        public void SetTransactions(List<TransactionModel> transactions)
        {
            //TODO: Finish this template
            string queryTemplate = "UPDATE [dbo].[Transaction] t SET Category = \"{0}\", Description = \"{1}\", Amount = {2}, Account_ID = {3}, Date = {4} IsEssential = {5} WHERE t.ID = " + transactions[0].ID + ";";
            string query = "";

            foreach (TransactionModel transaction in transactions)
            {
                //Category, Description, Amount, Account_ID, Date, IsEssential
                query += string.Format(queryTemplate, "Category", "Description",4.20,123,DateTime.Now,0);
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

        public void AddTransaction(TransactionModel transaction)
        {
            AddTransactions(new List<TransactionModel>() { transaction });
        }

        public void AddTransactions(List<TransactionModel> transactions)
        {

            string queryTemplate = "INSERT INTO [dbo].[Transaction] VALUES ({0},{1},{2},{3},{4},{5},{6});";
            string query = "";

            foreach (TransactionModel transaction in transactions)
            {
                //ID, Category, Description, Amount, Account_ID, Date, IsEssential
                query += string.Format(queryTemplate,124,"Category","Description",4.20,121,DateTime.Now,0);
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
}