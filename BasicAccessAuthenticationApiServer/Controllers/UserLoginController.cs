using BasicAccessAuthenticationApiServer.BusinessLogic.Interfaces;
using BasicAccessAuthenticationApiServer.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicAccessAuthenticationApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   
    public class UserLoginController : ControllerBase
    {
        private readonly IUserLoginLogic _userLoginLogic;
        private readonly ILogger<UserLoginController> _logger;
        public UserLoginController(IUserLoginLogic userLoginLogic, ILogger<UserLoginController> logger)
        {
            _userLoginLogic=userLoginLogic;
            _logger=logger;
        }
        public async Task<IActionResult> IsUserValid([FromForm] User user)
        {
            try
            {
              var reslt =await _userLoginLogic.IsValidUserAsync(user,HttpContext.RequestAborted);
                if (reslt!=null && reslt.IsValid == true)
                {
                    return Ok(reslt);
                }
                return BadRequest(new { HttpResponseMessage=StatusCodes.Status401Unauthorized});                
            }
            catch (Exception ex) {
                _logger.LogError(ex, "{Class} :Error while running IsUserValid {Exception}", nameof(UserLoginController), ex.Message);
                return BadRequest();
            }

        }
    }
}
