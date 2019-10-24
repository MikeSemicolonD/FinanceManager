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
                            ID = Utilities.ParseInt(reader["ID"].ToString())
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

        public TransactionModel GetTransactionsByID(long ID)
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
                            //TODO: Finish this
                            ID = Utilities.ParseInt(reader["ID"].ToString())
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
            //TODO: Generate query of params and ID's for each transaction
            string query = "UPDATE [dbo].[Transaction] t SET Param1 = 'asd', num = 11 WHERE t.ID = " + transactions[0].ID + ";";
            
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
            //TODO: Generate query of params for each transaction (Generate ID somehow?)
            string query = "INSERT INTO [dbo].[Transaction] VALUES (param1,param2);";

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