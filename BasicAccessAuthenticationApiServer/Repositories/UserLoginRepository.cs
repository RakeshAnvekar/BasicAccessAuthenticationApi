using BasicAccessAuthenticationApiServer.Constants;
using BasicAccessAuthenticationApiServer.Executor;
using BasicAccessAuthenticationApiServer.Mappers.Interfaces;
using BasicAccessAuthenticationApiServer.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BasicAccessAuthenticationApiServer.Repositories;

public class UserLoginRepository : IUserLoginRepository
{
    private readonly ILogger<UserLoginRepository> _logger;
    private readonly IDataBaseExecutor _dataBaseExecutor;
    private readonly IUserLoginMappers _userLoginMappers;
    public UserLoginRepository(ILogger<UserLoginRepository> logger, IDataBaseExecutor dataBaseExecutor, IUserLoginMappers userLoginMappers)
    {
        _logger = logger;
        _dataBaseExecutor = dataBaseExecutor;
        _userLoginMappers = userLoginMappers;
    }
    public async Task<bool> IsValisUserAsync(string UserName, string Password, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(UserName)) throw new ArgumentNullException(nameof(UserName));
        if (string.IsNullOrEmpty(Password)) throw new ArgumentNullException(nameof(Password));

        var inputParametrs = new Dictionary<string, object?>()
        {
            {"@UserName",UserName },
            {"@Password",Password },
          
        };
        var outputParametrs = new Dictionary<string, object?>()
        {
          { "@IsValid",false }
           

        };
        var result = await _dataBaseExecutor.ExecuteSpAsync(DbConstants.USP_IsValidUser, CommandType.StoredProcedure, _userLoginMappers.MapUserLogin, cancellationToken, inputParametrs, outputParametrs);
        if(result!=null && result.IsValid==true) return true;
        return false;
    }
}
