using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.API.Filters
{
    public class AuthFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Console.WriteLine("Hello");
            //base.OnActionExecuting(context); string key = string.Empty;

            var headers = context.HttpContext.Request.Headers;
            string token = headers["Authorization"];

            if (String.IsNullOrEmpty(token))
            {
                context.Result = new BadRequestObjectResult(new { message = "Bad request: Missing Token.", statusCode = HttpStatusCode.BadRequest });
                return;
            }

            #region Validate Token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var jwtoken = token.Replace("Bearer ", string.Empty);
            JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(jwtoken);
            if (jwtToken == null)
            {
                context.Result = new BadRequestObjectResult(new { message = "Wrong request", statusCode = HttpStatusCode.BadRequest });
                return;
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.TokenKey));
            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = key
            };
            SecurityToken securityToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(jwtoken, parameters, out securityToken);
            if (principal == null)
            {
                context.Result = new BadRequestObjectResult(new { message = "Bad request: Expired Token.", statusCode = HttpStatusCode.BadRequest });
                return;
            }
            #endregion

            #region Claim
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                context.Result = new BadRequestObjectResult(new { message = "Wrong request", statusCode = HttpStatusCode.BadRequest });
                return;
            }

            #endregion

            return;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }
    }
}
