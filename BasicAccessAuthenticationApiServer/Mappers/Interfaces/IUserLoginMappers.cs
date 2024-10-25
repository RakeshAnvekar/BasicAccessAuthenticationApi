using BasicAccessAuthenticationApiServer.Models.User;
using BasicAccessAuthenticationApiServer.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BasicAccessAuthenticationApiServer.Mappers.Interfaces;

public interface IUserLoginMappers
{
    public UserValid? MapUserLogin(SqlParameter[]? ouputParameters);
}
