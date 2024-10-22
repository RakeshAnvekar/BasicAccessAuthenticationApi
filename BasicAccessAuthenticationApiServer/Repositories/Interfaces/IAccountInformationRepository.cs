using BasicAccessAuthenticationApiServer.Models.Account;

namespace BasicAccessAuthenticationApiServer.Repositories.Interfaces;

public interface IAccountInformationRepository
{
    public Task<List<AccountInformation>> GetAsync(CancellationToken cancellationToken);
}
