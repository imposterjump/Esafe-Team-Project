using Esafe_Team_Project.Data.Enums;
using Esafe_Team_Project.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Esafe_Team_Project.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public Role role { get; set; }
        // added comment here 
        public AuthorizeAttribute(Role _role)
        {
            this.role = _role;
        }
        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
        {
            
            
            
            var user = (User)context.HttpContext.Items["Client"];

    


            if (user == null || user.Role != this.role)
            {

                // not logged in or role not authorized
                //addComment
                // adding a new comment here 
                context.Result = new JsonResult(new { message = "Unauthorized, Access Denied" }) { StatusCode = StatusCodes.Status401Unauthorized };

            }
        }
    }
}
