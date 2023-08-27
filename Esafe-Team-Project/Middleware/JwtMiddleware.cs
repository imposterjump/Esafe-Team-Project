using Esafe_Team_Project.Data;
using Esafe_Team_Project.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Esafe_Team_Project.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Jwt _jwt;
        private IConfiguration _config;

        public JwtMiddleware(RequestDelegate next, IOptions<Jwt> jwt, IConfiguration config)
        {
            _next = next;
            _jwt = jwt.Value;
            _config = config;
        }

        public async Task Invoke(HttpContext context, AppDbContext appDbContext)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                await attachAccountToContext(context, appDbContext, token);

            await _next(context);
        }

        private async Task attachAccountToContext(HttpContext context, AppDbContext appDbContext, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);


                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                Console.WriteLine(jwtToken.Claims.First(x => x.Type == "Role").Value);
                //var accountId = 4;
                // attach account to context on successful jwt validation

                var userrole = jwtToken.Claims.First(x => x.Type == "Role").Value;

                if (userrole == "Client")
                {

                    context.Items["Client"] = await appDbContext.Clients.Include(_ => _.ClientAddresses).FirstOrDefaultAsync(_ => _.Id == userId);
                }
                else
                {

                    context.Items["Client"] = await appDbContext.Admins.FirstOrDefaultAsync(_ => _.Id == userId);
                }



            }
            catch
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }

        }
    }
}
