using AutoMapper;
using Esafe_Team_Project.Data.Enums;
using Esafe_Team_Project.Data;
using Esafe_Team_Project.Entities;
using Microsoft.AspNetCore.Mvc;
using static Esafe_Team_Project.Helpers.AuthorizeAttribute;
using Esafe_Team_Project.Services;
using Esafe_Team_Project.Helpers;
using Esafe_Team_Project.Models;

namespace Esafe_Team_Project.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
        public class AdminController : BaseController
        {
            private readonly AppDbContext _dbContext;
            private readonly AdminServices _service;
            private readonly IMapper _mapper;
            private readonly ILogger<AdminController> _logger;

            public AdminController(AppDbContext dbContext, AdminServices service, IMapper mapper, ILogger<AdminController> logger)
            {
                _dbContext = dbContext;
                _service = service;
                _mapper = mapper;
                _logger = logger;
            }

            [Authorize(Role.Admin)]
            [HttpGet("test_authentication3")]
            public async Task<ActionResult> testingAuth2()
            {

                if (this.HttpContext.Response.StatusCode == 200)
                {
                    var client = Admin;
                    return Ok(client);
                }
                else
                {
                    return Content("sorry but unothorised or couldnt find content");
                }
            }

            [Authorize(Role.Admin)]
            [HttpGet("GetAllTranfers")]
            public async Task<ActionResult<List<Transfer>>> AdminGetAllTransfers()
            {
                try
                {
                    List<Transfer> transferDto = await _service.AdminGetTransferInfo();
                    return Ok(transferDto);

                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            [Authorize(Role.Admin)]
            [HttpGet("GetTranfersbyId")]
            public async Task<ActionResult<List<Transfer>>> AdminGetTransferInfoById(int id)
            {
                try
                {
                    var client = Client;
                    var transferDto = await _service.AdminGetTransferInfoById(client.Id);
                    return Ok(transferDto);

                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
        [Authorize(Role.Admin)]
        [HttpPost("approve_certificate")]
        public async Task<ActionResult<(string, Certificate)>> ApproveCertificate(int CertificateId)
        {
            var admin = Admin;
            var result = await _service.approveCertificate(CertificateId, admin.Id);
            return Ok(result);


        }

    }
    
}
