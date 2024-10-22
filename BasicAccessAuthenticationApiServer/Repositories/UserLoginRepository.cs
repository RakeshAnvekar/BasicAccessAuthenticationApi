using BasicAccessAuthenticationApiServer.Constants;
using BasicAccessAuthenticationApiServer.Executor;
using BasicAccessAuthenticationApiServer.Mappers.Interfaces;
using BasicAccessAuthenticationApiServer.Repositories.Interfaces;
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

      var result=  await _dataBaseExecutor.ExecuteQueryAsync(DbConstants.USP_IsValidUser, CommandType.StoredProcedure,_userLoginMappers.MapUserLogin, cancellationToken);
        if(result!=null && result.IsValid==true) return true;
        return false;
    }
}
