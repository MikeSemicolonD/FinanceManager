using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinanceManager.DAL
{
    public class SqlDataProvider : IDatabaseHandler
    {
        private static SqlConnection connection; 

        public SqlDataProvider()
        {
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
    
        }

        public SqlDataReader ExecuteReader(SqlCommand command)
        {
            return command.ExecuteReader();
        }

        public SqlCommand CreateCommand(string query, SqlConnection connection)
        {
            return (SqlCommand) CreateCommand(query, CommandType.Text, connection);
        }

        public IDbConnection CreateConnection()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            return connection;
        }

        public IDbConnection GetConnection()
        {
            return connection;
        }

        public void CloseConnection(IDbConnection connection)
        {
            connection.Close();
        }

        public IDbCommand CreateCommand(string CommandText, CommandType commandType, IDbConnection connection)
        {
            return new SqlCommand(CommandText, (SqlConnection) connection);
        }

        public IDataAdapter CreateAdapter(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        public IDbDataParameter CreateParameter(string name, object value)
        {
            return new SqlParameter(name, value);
        }
    }
}