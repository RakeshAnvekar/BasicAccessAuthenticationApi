using BasicAccessAuthenticationApiServer.Models.User;
using BasicAccessAuthenticationApiServer.Repositories;

namespace BasicAccessAuthenticationApiServer.BusinessLogic.Interfaces;

public interface IUserLoginLogic
{
    public Task<UserValid?> IsValidUserAsync(User user,CancellationToken cancellationToken);
}
