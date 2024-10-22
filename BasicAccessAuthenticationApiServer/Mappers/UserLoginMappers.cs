using BasicAccessAuthenticationApiServer.Mappers.Interfaces;
using BasicAccessAuthenticationApiServer.Models.User;
using System.Data;

namespace BasicAccessAuthenticationApiServer.Mappers
{
    public sealed class UserLoginMappers : IUserLoginMappers
    {
        public UserValid? MapUserLogin(IDataReader reader)
        {

            if(!reader.Read()) return null;

            return new UserValid
            {
                IsValid = reader["IsValid"]!=DBNull.Value?(bool)reader["IsValid"]:false
            };
           
        }
    }
}
