using BasicAccessAuthenticationApiServer.Models.User;
using BasicAccessAuthenticationApiServer.Repositories;
using System.Data;

namespace BasicAccessAuthenticationApiServer.Mappers.Interfaces;

public interface IUserLoginMappers
{
    public UserValid? MapUserLogin(IDataReader reader);
}
