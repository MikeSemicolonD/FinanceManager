using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using FinanceManager.Models;
using FinanceManager.DAL;

public class TransactionsAdapter
{
    /// <summary>
    /// Returns a list of transactions for a given user
    /// </summary>
    /// <param name="UID"></param>
    /// <returns></returns>
    public List<TransactionModel> GetTransactionsByUID(string UID)
    {
        string query = "SELECT * FROM [dbo].[Transaction] t left join [dbo].[User_Transactions] ut on t.ID = ut.UID Where ut.UID = '" + UID + "';";

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
                        ID = Utilities.ParseInt(reader["ID"].ToString()),
                        Description = reader["Description"].ToString(),
                        IsEssential = Utilities.ParseBool(reader["IsEssential"].ToString()),
                        Category = reader["Category"].ToString(),
                        Price = Utilities.ParseDecimal(reader["Amount"].ToString()),
                        AccountID = Utilities.ParseInt(reader["Account_ID"].ToString()),
                        AccountType = reader["AccountType"].ToString()
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

        //Create a string full of all transaction IDs to delete
        for (int i = 0; i < transactions.Count; i++)
        {
            IDs += transactions[i].ID.ToString();

            if (i != transactions.Count - 1)
            {
                IDs += ',';
            }
        }

        string query = string.Format(template, IDs);

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

    public void DeleteTransactionByID(long ID)
    {
        string query = "DELETE * FROM [dbo].[Transaction] t WHERE t.ID = " + ID + ";";
        
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
                        Description = reader["Description"].ToString(),
                        IsEssential = Utilities.ParseBool(reader["IsEssential"].ToString()),
                        Category = reader["Category"].ToString(),
                        Price = Utilities.ParseDecimal(reader["Amount"].ToString()),
                        AccountID = Utilities.ParseInt(reader["Account_ID"].ToString()),
                        AccountType = reader["AccountType"].ToString()
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
        string queryTemplate = "UPDATE [dbo].[Transaction] t SET Description = '{0}', IsEssential = {1}, Category = '{2}', Amount = {3}, Account_ID = {4}, AccountType = '{5}' WHERE t.ID = {6}; ";
        string query = "";

        foreach (TransactionModel transaction in transactions)
        {
            //Description, IsEssential, Category, Price, Account_ID, AccountType, ID
            query += string.Format(queryTemplate, transaction.Description, transaction.IsEssential, transaction.Category, transaction.Price, transaction.AccountID, transaction.AccountType, transaction.ID);
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

    public void AddTransaction(TransactionModel transaction, string UserEmail)
    {
        AddTransactions(new List<TransactionModel>() { transaction }, UserEmail);
    }

    public void AddTransactions(List<TransactionModel> transactions, string UserEmail)
    {

        string queryTemplate = "INSERT INTO [dbo].[Transaction] VALUES ('{0}',{1},'{2}',{3},{4},'{5}');";
        string query = "";

        List<int> NewTransactionIDs = new List<int>();

        foreach (TransactionModel transaction in transactions)
        {
            //Name, Description, IsEssential, Category, Price, Account_ID, AccountType
            query += string.Format(queryTemplate, transaction.Description, transaction.IsEssential, transaction.Category, transaction.Price, transaction.AccountID, transaction.AccountType);
        }

        //Returns the ID of the transactions we just created
        query += " SELECT SCOPE_IDENTITY() AS [NewIDs];";

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
                    NewTransactionIDs.Add(Utilities.ParseInt(reader["NewIDs"].ToString()));
                }

                reader.Close();
                
                string template = "INSERT INTO [dbo].[User_Transactions] VALUES('{0}',{1}); ";

                string UTQuery = "";

                string UserUID = Utilities.GetUsersUID(UserEmail);

                foreach (int id in NewTransactionIDs)
                {
                    UTQuery += string.Format(template,UserUID,id);
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