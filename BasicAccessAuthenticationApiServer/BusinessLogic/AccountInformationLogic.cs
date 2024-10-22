using BasicAccessAuthenticationApiServer.BusinessLogic.Interfaces;
using BasicAccessAuthenticationApiServer.Models.Account;
using BasicAccessAuthenticationApiServer.Repositories.Interfaces;

namespace BasicAccessAuthenticationApiServer.BusinessLogic;

public sealed class AccountInformationLogic : IAccountInformationLogic
{
    private readonly IAccountInformationRepository _accountInformationRepository;
    public AccountInformationLogic(IAccountInformationRepository accountInformationRepository)
    {
        _accountInformationRepository = accountInformationRepository;

    }
    public async Task<List<AccountInformation>> GetAsync(CancellationToken cancellationToken)
    {
        return await _accountInformationRepository.GetAsync(cancellationToken);
    }
}
