using AutoMapper;
using Esafe_Team_Project.Data;
using Esafe_Team_Project.Data.Enums;
using Esafe_Team_Project.Entities;
using Esafe_Team_Project.Helpers;
using Esafe_Team_Project.Models;
using Esafe_Team_Project.Models.Address;
using Esafe_Team_Project.Models.Client.Request;
using Esafe_Team_Project.Models.Client.Response;
using Esafe_Team_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static Esafe_Team_Project.Helpers.AuthorizeAttribute;

namespace Esafe_Team_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : BaseController
    {
        private readonly AppDbContext _dbContext;
        private readonly ClientService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientController> _logger;
        public ClientController(AppDbContext dbContext, ClientService service, IMapper mapper, ILogger<ClientController> logger)
        {
            _dbContext = dbContext;
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ClientDisplayDto>> GetClients()
        {
            //Using Service:
            Console.WriteLine("entered get clients controller");
            var data = await _service.GetAll();
            if (data == null)
            {
                return NotFound();
            }
            _logger.LogInformation("Get clients is successful");
            return Ok(data);
        }

        [Authorize(Data.Enums.Role.Client)]
        [HttpGet("AuthorizedGet")]
        public async Task<ActionResult> GetClientAuth()
        {
            try
            {
                var client = Client;
                var clientDto = _service.GetClientAuth(client);
                return Ok(clientDto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetClientBy{id}")]
        public async Task<ActionResult<List<ClientDisplayDto>>> GetClientbyID(int id)
        {
            //Using Service:

            var data = await _service.GetClientbyID(id);
            if (data == null)
            {
                return NotFound();
            }

            _logger.LogInformation("Get client with id = {id} is successful", id);
            return data;

        }

        [HttpPost("AddClient")]
        public async Task<IActionResult> AddClient(ClientDto client)
        {
            try
            {
                Console.WriteLine("entered client controller");

                var clientResp = await _service.AddClient(client);

                _logger.LogInformation("Client added successfully");

                return Created($"/client/{clientResp.NationalId}", clientResp);

            }
            catch
            {
                return StatusCode(500, "An error occured while adding client");
            }
        }

        [HttpPost("Register")]

        public async Task<IActionResult> Register(ClientDto client)
        {
            try
            {
                _service.Register(client);
                return Ok();
            }
            catch
            {
                return StatusCode(500, "An error occured while registering");
            }
        }

        [HttpPost("AddAddress")]
        public async Task<ActionResult> AddAddress(int id, AddressDto address)
        {
            if (address != null)
            {
                if (ClientAvail(id) == false) { return NotFound(); }
                else
                {
                    var updatedClient = _service.AddAddress(id, address);
                    if (updatedClient == null)
                    {
                        return StatusCode(500, "Error occured while adding address");
                    }
                    return Ok(updatedClient);


                }
            }
            else
            {
                return StatusCode(400, "Input is empty");
            }

        }

        [Authorize(Data.Enums.Role.Client)]
        [HttpPut("UpdatePassword")]
        public async Task<ActionResult<Client>> UpdateClient(string password, string confpassword)
        {
            if (!password.IsNullOrEmpty() || !confpassword.IsNullOrEmpty())
            {
                if (password.Length >= 8 && confpassword.Length >= 8)
                {
                    var client = Client;
                    await _service.UpdatePassword(password, client);
                    _logger.LogInformation("Client Password updated successfully");
                    return Ok("Password updated successfully");
                }
                return StatusCode(500, "Password and confirm password lengths invalid.");
            }
            return StatusCode(500, "Fields cannot be empty.");

        }

        private bool ClientAvail(int id)
        {
            return (_dbContext.Clients?.Any(x => x.Id == id)).GetValueOrDefault();
        }


        [HttpDelete]
        public async Task<ActionResult<Client>> DeleteClient(int id)
        {
            if (_dbContext.Clients == null)
            {
                return NotFound();
            }

            var client = await _dbContext.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            _dbContext.Clients.Remove(client);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Data.Enums.Role.Client)]
        [HttpGet("GetTransfers")]
        public async Task<ActionResult<List<TransferResponse>>> GetTransferInfo()
        {
            try
            {
                var client = Client;
                List<TransferResponse> transferDto = await _service.GetTransferInfo(client);
                return Ok(transferDto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Data.Enums.Role.Client)]
        [HttpGet("GetTranferBy{id}")]
        public async Task<ActionResult<List<TransferResponse>>> GetTransferInfoById(int id)
        {
            //Using Service:

            var data = await _service.GetTransferInfoById(id);
            if (data == null)
            {
                return NotFound();
            }

            _logger.LogInformation("Get transfers with id = {id} is successful", id);
            return data;

        }




        [Authorize(Role.Client)]
        [HttpGet("GetCreditCards")]
        public async Task<ActionResult<List<CreditCardDto>>> GetCreditCardDetails()
        {
            try
            {
                var client = Client;
                List<CreditCardDto> cards = await _service.GetCreditDets(client);
                if (cards != null)
                {
                    return Ok(cards);
                }
                else
                {
                    return NotFound("You don't have any credit cards");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);

            }
        }

        [Authorize(Role.Client)]
        [HttpGet("GetCertificates")]
        public async Task<ActionResult<List<CertificateDto>>> GetCertificateDetails()
        {
            try
            {
                var client = Client;
                List<CertificateDto> cards = await _service.GetCertificateDets(client);
                if (cards != null)
                {
                    return Ok(cards);
                }
                else
                {
                    return NotFound("You don't have any certificates");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);

            }
        }
        [Authorize(Role.Client)]
        [HttpPost("transfer_money")]
        public async Task<ActionResult<Transfer>> transfer_money(double amount , int receiver_id)
        {
            var client= Client;
            var result = await _service.tranfermoney(amount, client.Id, receiver_id);
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("error in the transaction ");
            }
        }

        [Authorize(Role.Client)]
        [HttpPost("apply_for_a_certificate")]
        public async Task<ActionResult<(string,Certificate)>> certificate_app(CertificateReqDto c1)
        
        {
            Console.WriteLine(" i am in the controller");
            var client = Client;
            var c2 = _mapper.Map<Certificate>(c1);
            var result = await _service.add_certificate(client.Id, c2);
            return Ok(result);
        }

        [Authorize(Role.Client)]
        [HttpPost("CreditCardApplication (Silver,Gold,Platinum)")]
        public async Task<ActionResult> CreditCardApp(CreditCardReqDto creditCardReq)

        {
            var client = Client;
            var creditCard = _mapper.Map<CreditCard>(creditCardReq);
            var result = await _service.addCreditCard(client.Id, creditCard);
            return Ok("your application is pending");
        }


    }




}
