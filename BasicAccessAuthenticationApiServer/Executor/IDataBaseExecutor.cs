using Microsoft.Data.SqlClient;
using System.Data;
namespace BasicAccessAuthenticationApiServer.Executor;
public interface IDataBaseExecutor
{
    Task ExecuteAsync(string sql,CommandType commandType, CancellationToken cancellationToken,Dictionary<string,object>? parametters=null);
    Task<T> ExecuteQueryAsync<T>(string sql, CommandType commandType, Func<IDataReader, T> mapper, CancellationToken cancellation, Dictionary<string, object?>? parameters = null);
    Task<T> ExecuteSpAsync<T>(string sql, CommandType commandType, Func<SqlParameter[], T> mapper, CancellationToken cancellation, Dictionary<string, object?>? inputParameters = null, Dictionary<string, object?>? outputParameters = null);
}
