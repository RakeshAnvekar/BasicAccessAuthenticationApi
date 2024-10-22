using BasicAccessAuthenticationApiServer.Constants;
using BasicAccessAuthenticationApiServer.Executor;
using BasicAccessAuthenticationApiServer.Mappers.Interfaces;
using BasicAccessAuthenticationApiServer.Models.Account;
using BasicAccessAuthenticationApiServer.Repositories.Interfaces;
using System.Data;

namespace BasicAccessAuthenticationApiServer.Repositories;

public sealed class AccountInformationRepository : IAccountInformationRepository
{
    private readonly IDataBaseExecutor _dataBaseExecutor;
    private readonly ILogger<AccountInformationRepository> _logger;
    private readonly IAccountInformationMapper _accountInformationMapper;
    public AccountInformationRepository(IDataBaseExecutor dataBaseExecutor, ILogger<AccountInformationRepository> logger, IAccountInformationMapper accountInformationMapper)
    {
        _dataBaseExecutor = dataBaseExecutor;
        _logger = logger;
        _accountInformationMapper = accountInformationMapper;
    }
    public async Task<List<AccountInformation>> GetAsync(CancellationToken cancellationToken)
    {
        

        var items = await _dataBaseExecutor.ExecuteQueryAsync(DbConstants.USP_GetAcccountDetails, CommandType.StoredProcedure,_accountInformationMapper.MapAccountInformations, cancellationToken);
        return items;
    }
}
