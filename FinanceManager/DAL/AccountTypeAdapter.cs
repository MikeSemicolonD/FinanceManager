using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using FinanceManager.Models;
using FinanceManager.DAL;

public class AccountTypeAdapter
{
    /// <summary>
    /// Returns a list of AccountTypes for a given user
    /// </summary>
    /// <param name="UID"></param>
    /// <returns></returns>
    public List<AccountTypeModel> GetAccountTypesByUID(string UID)
    {
        string query = "SELECT * FROM [dbo].[User_Accounts] UA left join [dbo].[Account] A on UA.Account_ID = A.ID Where UA.UID = " + UID + ";";

        List<AccountTypeModel> AccountTypes = new List<AccountTypeModel>();
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
                    AccountTypeModel AccountType = new AccountTypeModel()
                    {
                        ID = Utilities.ParseInt(reader["ID"].ToString()),
                        AccountType = reader["AccountType"].ToString()
                    };

                    AccountTypes.Add(AccountType);
                }

                reader.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return AccountTypes;
    }

    public void DeleteAccountTypes(List<AccountTypeModel> accountTypes)
    {
        //Parse each id into query
        string template = "DELETE * FROM [dbo].[Account] a WHERE a.ID IN ({0}); DELETE * FROM [dbo].[User_Accounts] UA WHERE UA.Account_ID IN ({0});";
        string IDs = "";

        //Create a string full of all Account Types IDs to delete
        for (int i = 0; i < accountTypes.Count; i++)
        {
            IDs += accountTypes[i].ID.ToString();

            if (i != accountTypes.Count - 1)
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

    public void DeleteAccountTypeByID(long ID)
    {
        string query = "DELETE * FROM [dbo].[Account] a WHERE a.ID = "+ID+ "; DELETE * FROM [dbo].[User_Accounts] UA WHERE UA.Account_ID = " + ID + ";";
        
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

    public AccountTypeModel GetAccountTypeByID(long ID)
    {
        string query = "SELECT * FROM [dbo].[Account] a WHERE a.ID = " + ID + ";";

        AccountTypeModel accountType = new AccountTypeModel();
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
                    accountType = new AccountTypeModel()
                    {
                        ID = Utilities.ParseInt(reader["ID"].ToString()),
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

        return accountType;
    }

    public void AddAccountType(AccountTypeModel AccountType)
    {
        AddAccountTypes(new List<AccountTypeModel>() { AccountType });
    }

    public void AddAccountTypes(List<AccountTypeModel> AccoutTypes)
    {

        string queryTemplate = "INSERT INTO [dbo].[Account] VALUES ({0});";
        string query = "";

        List<int> NewAccountTypeIDs = new List<int>();

        foreach (AccountTypeModel Account in AccoutTypes)
        {
            //AccountType
            query += string.Format(queryTemplate, Account.AccountType);
        }

        //Returns the ID of the AccoutTypes we just created
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
                    NewAccountTypeIDs.Add(Utilities.ParseInt(reader["NewIDs"].ToString()));
                }

                reader.Close();
                
                string template = "INSERT INTO [dbo].[User_Accounts] VALUES({0},{1}); ";

                string UAQuery = "";

                string UserUID = Utilities.GetUsersUID();

                foreach (int id in NewAccountTypeIDs)
                {
                    UAQuery += string.Format(template,UserUID,id);
                }

                SqlCommand UTcommand = db.CreateCommand(UAQuery, connection);

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