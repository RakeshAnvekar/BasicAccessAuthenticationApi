namespace BasicAccessAuthenticationApiServer.Repositories.Interfaces;

public interface IUserLoginRepository
{
    public Task<bool> IsValisUserAsync(string UserName,string Password,CancellationToken cancellation);
}
