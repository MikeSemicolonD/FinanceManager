using System.Data;
namespace FinanceManager.DAL
{
    public interface IDatabaseHandler
    {
        IDbConnection CreateConnection();
        IDbConnection GetConnection();
        void CloseConnection(IDbConnection connection);
        IDbCommand CreateCommand(string CommandText, CommandType commandType, IDbConnection connection);
        IDataAdapter CreateAdapter(IDbCommand command);
        IDbDataParameter CreateParameter(string name, object value);
    }
}