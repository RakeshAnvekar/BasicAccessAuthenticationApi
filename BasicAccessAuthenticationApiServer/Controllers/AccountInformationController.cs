using BasicAccessAuthenticationApiServer.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BasicAccessAuthenticationApiServer.Filters;

namespace BasicAccessAuthenticationApiServer.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountInformationController : ControllerBase
{
    private readonly ILogger<AccountInformationController> _logger;
    private readonly IAccountInformationLogic _accountInformationLogic;
    public AccountInformationController(ILogger<AccountInformationController> logger, IAccountInformationLogic accountInformationLogic)
    {
        _logger = logger;
        _accountInformationLogic = accountInformationLogic;
    }
    [HttpGet]
    [ActionName("GetAccontDetails")]   
    [ServiceFilter(typeof(BasicAuthenticationFilter))]
    public async Task<IActionResult> Get()
    {
        try
        {
            var items= await _accountInformationLogic.GetAsync(HttpContext.RequestAborted);
            return Ok(items);
        }
        catch (Exception ex) {
            _logger.LogError(ex,"{Class} : Error getting account information. Excaption Message= {ExceptionMessage}",
                nameof(AccountInformationController),
                ex.Message );
            return BadRequest();
        }
    }

}
