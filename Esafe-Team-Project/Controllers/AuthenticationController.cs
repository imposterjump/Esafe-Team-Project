using Esafe_Team_Project.Models.Client.Request;
using Esafe_Team_Project.Models.Client.Response;
using Esafe_Team_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace Esafe_Team_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ClientService _service;
        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController(ClientService service, ILogger<AuthenticationController> logger)
        {
            _service = service;
            _logger = logger;

        }

        [HttpPost("Authenticate")]

        public ActionResult<AuthenticateResponse> Authenticate(ClientLoginDto client)
        {
            try
            {
                var response = _service.Authenticate(client, ipAddress());
                _logger.LogInformation("client logged in.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());

            }

        }






        //Helpers
        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }

}
