using System.Data;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Threading;
using BasicAccessAuthenticationApiServer.Models.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace BasicAccessAuthenticationApiServer.Executor;

public sealed class DataBaseExecutor : IDataBaseExecutor
{
    #region Fields
    private readonly ILogger<DataBaseExecutor> _logger;
    private readonly ConnectionOptions _connectionOptions;
    #endregion
    #region Constructor
    public DataBaseExecutor(ILogger<DataBaseExecutor> logger, IOptions<ConnectionOptions> connectionOptions)
    {
        _logger = logger;
        _connectionOptions = connectionOptions.Value;
    }
    #endregion
    public async Task ExecuteAsync(string sql, CommandType commandType, CancellationToken cancellationToken, Dictionary<string, object>? parametters = null)
    {
        try
        {
            var stopWatch = Stopwatch.StartNew();
            await using var connection = new SqlConnection(_connectionOptions.ConnectionString);
            await using var command = connection.CreateCommand();
            await connection.OpenAsync();
            command.CommandText = sql;
            command.CommandType = commandType;
            command.CommandTimeout = 60 * 30;
            if (parametters != null)
            {
                foreach (var key in parametters.Keys)
                {
                    command.Parameters.AddWithValue(key, parametters[key] == null ? DBNull.Value : parametters[key]);

                }
                await command.ExecuteNonQueryAsync(cancellationToken);
                connection.Close();
            }
        }
        catch(Exception ex)
        {
            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("Operation cancelled by user", ex, cancellationToken);
            _logger.LogError(ex, "SQL Server read query error");
        }       
    }

    public async Task<T> ExecuteQueryAsync<T>(string sql, CommandType commandType, Func<IDataReader, T> mapper, CancellationToken cancellation, Dictionary<string, object?>? parameters = null)
    {
        try
        {
            var stopWatch = Stopwatch.StartNew();
            await using var connection = new SqlConnection(_connectionOptions.ConnectionString);
            await using var command = connection.CreateCommand();
            await connection.OpenAsync();
            command.CommandText = sql;
            command.CommandType = commandType;
            command.CommandTimeout = 60 * 30;
            if (parameters != null)
            {
                foreach (var key in parameters.Keys)
                {
                    command.Parameters.AddWithValue(key, parameters[key] == null ? DBNull.Value : parameters[key]);

                }    
            }
          
            var reader = await command.ExecuteReaderAsync(cancellation);
          
            var results = mapper(reader);
            return results;
        }
        catch (Exception ex)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException("Operation cancelled by user", ex, cancellation);
            _logger.LogError(ex, "SQL Server read query error");
            throw;
        }
    }
    public async Task<T> ExecuteSpAsync<T>(string sql, CommandType commandType, Func<SqlParameter[], T> mapper, CancellationToken cancellation, Dictionary<string, object?>? inputParameters = null, Dictionary<string, object?>? outputParameters = null)
    {
        try
        {
            var stopWatch = Stopwatch.StartNew();
            await using var connection = new SqlConnection(_connectionOptions.ConnectionString);
            await using var command = connection.CreateCommand();
            await connection.OpenAsync();
            command.CommandText = sql;
            command.CommandType = commandType;
            command.CommandTimeout = 60 * 30;
            if (inputParameters != null)
            {
                foreach (var key in inputParameters.Keys)
                {
                    command.Parameters.AddWithValue(key, inputParameters[key] == null ? DBNull.Value : inputParameters[key]);

                }
            }
            var outputParameterList = new List<SqlParameter>();
            if (outputParameters!=null)
            {
                SqlParameter outputParam;
                foreach (var key in outputParameters.Keys)
                {

                    outputParam = new SqlParameter(key, outputParameters[key])
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);
                    outputParameterList.Add(outputParam);
                }
            }
           

            var reader = await command.ExecuteNonQueryAsync(cancellation);

            var results = mapper(outputParameterList.ToArray());
            connection.Close();
            return results;
        }
        catch (Exception ex)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException("Operation cancelled by user", ex, cancellation);
            _logger.LogError(ex, "SQL Server read query error");
            throw;
        }
    }
}
