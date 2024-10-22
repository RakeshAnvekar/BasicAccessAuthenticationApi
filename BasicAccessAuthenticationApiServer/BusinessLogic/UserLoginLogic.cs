using BasicAccessAuthenticationApiServer.BusinessLogic.Interfaces;
using BasicAccessAuthenticationApiServer.Models.User;
using BasicAccessAuthenticationApiServer.Repositories;
using BasicAccessAuthenticationApiServer.Repositories.Interfaces;

namespace BasicAccessAuthenticationApiServer.BusinessLogic
{
    public class UserLoginLogic : IUserLoginLogic
    {
        private readonly IUserLoginRepository _userLoginRepository;
        public UserLoginLogic(IUserLoginRepository userLoginRepository)
        {
            _userLoginRepository=userLoginRepository;
        }
       

        async  Task<UserValid?> IUserLoginLogic.IsValidUserAsync(User user, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            if (string.IsNullOrEmpty(user.UserName)) throw new ArgumentNullException(nameof(user.UserName));
            if (string.IsNullOrEmpty(user.Password) ) throw new ArgumentNullException(nameof(user.UserName));
           
            var userValidObj=new UserValid();
            userValidObj.IsValid= await  _userLoginRepository.IsValisUserAsync(user.UserName,user.Password,cancellationToken);
            return userValidObj;
        }
    }
}
