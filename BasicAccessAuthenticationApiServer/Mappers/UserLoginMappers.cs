using BasicAccessAuthenticationApiServer.Mappers.Interfaces;
using BasicAccessAuthenticationApiServer.Models.User;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BasicAccessAuthenticationApiServer.Mappers
{
    public sealed class UserLoginMappers : IUserLoginMappers
    {
        
        public UserValid? MapUserLogin(SqlParameter[]? ouputParameters)
        {
            var userResponse= new UserValid();
            if (ouputParameters!=null) {
                foreach (var param in ouputParameters)
                {
                    switch (param.ParameterName)
                    {
                        case "@IsValid":
                            userResponse.IsValid=(bool)param.Value;
                            break;
                    }
                }
            }
           return userResponse;
           
           
        }
    }
}
