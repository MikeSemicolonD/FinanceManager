using System;
using System.Configuration;
using System.Data.SqlClient;

namespace FinanceManager.DAL
{
    public class SqlDataProvider
    {
        private static SqlConnection connection; 

        public SqlDataProvider()
        {
            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
                connection = new SqlConnection(ConnectionString);
            }
            catch(Exception e)
            {
                throw e;
            }
    
        }

        public SqlParameter CreateSqlParameter(string name, object value)
        {
            return new SqlParameter(name, value);
        }

        public SqlDataReader ExecuteReader(SqlCommand command)
        {
            return command.ExecuteReader();
        }

        public SqlCommand CreateCommand(string query, SqlConnection connection)
        {
            return new SqlCommand(query, connection);
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }

    }
}