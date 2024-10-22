using BasicAccessAuthenticationApiServer.Models.Account;

namespace BasicAccessAuthenticationApiServer.BusinessLogic.Interfaces;

public interface IAccountInformationLogic
{
    public Task<List<AccountInformation>> GetAsync(CancellationToken cancellationToken);
}
