using Esafe_Team_Project.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Esafe_Team_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        public Client Client => (Client)HttpContext.Items["Client"];
        public Admin Admin => (Admin)HttpContext.Items["Client"];



    }
}
