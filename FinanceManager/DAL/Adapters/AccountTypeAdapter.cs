using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using FinanceManager.Models;

namespace FinanceManager.DAL
{
    public class AccountTypeAdapter
    {
        /// <summary>
        /// Returns a list of AccountTypes for a given user
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public List<AccountTypeModel> GetAccountTypesByUID(string UID)
        {
            List<AccountTypeModel> AccountTypes = new List<AccountTypeModel>();

            try
            {
                SqlDataProvider db = new SqlDataProvider();
                string query = "SELECT ID, Type FROM [dbo].[User_Accounts] UA left join [dbo].[Account] A on UA.Account_ID = A.ID Where UA.UID = '" + UID + "';";

                using (SqlConnection connection = (SqlConnection) db.GetConnection())
                {
                    connection.Open();

                    SqlCommand command = db.CreateCommand(query, connection);
                    SqlDataReader reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        AccountTypeModel AccountType = new AccountTypeModel()
                        {
                            ID = Utilities.ParseInt(reader["ID"].ToString()),
                            AccountType = reader["Type"].ToString()
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

        /// <summary>
        /// Deletes records from [User_Accounts] where the ID matches
        /// ([User_Accounts] relates [Account] to [ASP_NET_User])
        /// </summary>
        /// <param name="accountTypes"></param>
        public void DeleteAccountTypes(List<AccountTypeModel> accountTypes)
        {
            try
            {
                SqlDataProvider db = new SqlDataProvider();

                //Parse each id into query
                string template = "DELETE FROM [dbo].[User_Accounts] UA WHERE UA.Account_ID IN ({0});";
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

                using (SqlConnection connection = (SqlConnection) db.GetConnection())
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
        /// Deletes record from [User_Accounts] where the ID matches
        /// ([User_Accounts] relates [Account] to [ASP_NET_User])
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteAccountTypeByID(long ID)
        {
            try
            {
                SqlDataProvider db = new SqlDataProvider();

                string query = "DELETE FROM [dbo].[User_Accounts] WHERE Account_ID = " + ID + ";";

                using (SqlConnection connection = (SqlConnection) db.GetConnection())
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
        /// Delete record from [Account] where the ID matches. 
        /// (Deletes from [Account] and not [User_Accounts] which relates [Account] to [ASP_NET_User])
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public AccountTypeModel GetAccountTypeByID(long ID)
        {
            AccountTypeModel accountType = new AccountTypeModel();

            try
            {
                SqlDataProvider db = new SqlDataProvider();

                string query = "SELECT * FROM [dbo].[Account] a WHERE a.ID = " + ID + ";";

                using (SqlConnection connection = (SqlConnection) db.GetConnection())
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

        /// <summary>
        /// Adds to [Account] if the types don't already exist.
        /// Also adds reference to account types into [User_Accounts]. (Links the account types to the user)
        /// </summary>
        /// <param name="AccountType"></param>
        /// <param name="UserEmail"></param>
        public void AddAccountType(AccountTypeModel AccountType, string UserEmail)
        {
            AddAccountTypes(new List<AccountTypeModel>() { AccountType }, UserEmail);
        }

        /// <summary>
        /// Adds to [Account] if the type doesn't already exists.
        /// Also adds reference to account type into [User_Accounts]. (Links the account type to the user)
        /// </summary>
        /// <param name="AccoutTypes"></param>
        /// <param name="UserEmail"></param>
        public void AddAccountTypes(List<AccountTypeModel> AccoutTypes, string UserEmail)
        {

            SqlDataProvider db = new SqlDataProvider();

            try
            {
                using (SqlConnection connection = (SqlConnection) db.GetConnection())
                {
                    connection.Open();

                    string preCheck = "SELECT ID FROM [dbo].[Account] WHERE Type LIKE {0};";
                    string preCheckConditions = "";
                    string query = "";

                    for (int i = 0; i < AccoutTypes.Count; i++)
                    {
                        preCheckConditions += ("'" + AccoutTypes[i].ID + "'");

                        if (AccoutTypes.Count > 1 && i < AccoutTypes.Count - 1)
                        {
                            preCheckConditions += " OR ";
                        }
                    }

                    query = string.Format(preCheck, preCheckConditions);

                    SqlCommand preCheckCommand = db.CreateCommand(query, connection);

                    SqlDataReader preCheckReader = db.ExecuteReader(preCheckCommand);

                    List<int> PotentialDuplicateAccountTypeIDs = new List<int>();

                    while (preCheckReader.Read())
                    {
                        PotentialDuplicateAccountTypeIDs.Add(Utilities.ParseInt(preCheckReader["ID"].ToString()));
                    }

                    preCheckReader.Close();

                    List<AccountTypeModel> NonDuplicateAccoutTypes = new List<AccountTypeModel>();

                    for (int i = 0; i < AccoutTypes.Count; i++)
                    {
                        if (!PotentialDuplicateAccountTypeIDs.Contains(AccoutTypes[i].ID))
                        {
                            NonDuplicateAccoutTypes.Add(AccoutTypes[i]);
                        }
                    }

                    List<int> AccountTypeIDs = new List<int>();

                    if (NonDuplicateAccoutTypes.Count > 0)
                    {
                        string queryTemplate = "INSERT INTO [dbo].[Account] VALUES ('{0}');";
                        query = "";

                        foreach (AccountTypeModel Account in NonDuplicateAccoutTypes)
                        {
                            //AccountType
                            query += string.Format(queryTemplate, Account.AccountType);
                        }

                        //Returns the ID of the AccoutTypes we just created
                        query += " SELECT SCOPE_IDENTITY() AS [NewIDs];";

                        SqlCommand command = db.CreateCommand(query, connection);

                        SqlDataReader reader = db.ExecuteReader(command);

                        while (reader.Read())
                        {
                            AccountTypeIDs.Add(Utilities.ParseInt(reader["NewIDs"].ToString()));
                        }

                        reader.Close();
                    }

                    string template = "INSERT INTO [dbo].[User_Accounts] VALUES('{0}',{1}); ";

                    string UAQuery = "";

                    string UserUID = Utilities.GetUsersUID(UserEmail);

                    foreach (int id in AccountTypeIDs)
                    {
                        UAQuery += string.Format(template, UserUID, id);
                    }

                    SqlCommand UTcommand = db.CreateCommand(UAQuery, connection);

                    UTcommand.ExecuteScalar();

                    preCheckReader.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}