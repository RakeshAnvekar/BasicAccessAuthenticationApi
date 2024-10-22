using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace BasicAccessAuthenticationApiServer.Filters;

public  class BasicAuthenticationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            context.Result = new  UnauthorizedResult();
            return;
        }
        if (!authHeader.ToString().StartsWith("Basic")) {
            context.Result = new UnauthorizedResult();
            return;
        }
        var encodedCredentials = authHeader.ToString().Substring("Basic ".Length).Trim();
        var credentialBytes = Convert.FromBase64String(encodedCredentials);
        var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

        if (credentials.Length != 2 || !IsValidUser(credentials[0], credentials[1]))
        {
            context.Result = new UnauthorizedResult();
        }

         bool IsValidUser(string username, string password)
        {
            return username == "yourUsername" && password == "yourPassword";
        }
    }
}




//Go to the Headers tab.

//Add a new header:

//Key: Authorization
//Value: Basic followed by the Base64-encoded string of your credentials.
//To encode manually: echo -n "rakesh:rakesh12" | base64 (in a terminal) gives you the Base64 value.