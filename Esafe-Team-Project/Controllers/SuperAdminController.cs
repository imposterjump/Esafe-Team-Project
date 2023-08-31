using AutoMapper;
using Esafe_Team_Project.Data;
using Esafe_Team_Project.Data.Enums;
using Esafe_Team_Project.Entities;
using Esafe_Team_Project.Helpers;
using Esafe_Team_Project.Models;
using Esafe_Team_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Esafe_Team_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : BaseController
    {
        private readonly AppDbContext _dbContext;
        private readonly SuperAdminServices _service;
        private readonly IMapper _mapper;
        public SuperAdminController(AppDbContext dbContext, SuperAdminServices service, IMapper mapper)
        {
            _dbContext = dbContext;
            _service = service;
            _mapper = mapper;
        }

        [Authorize(Role.SuperAdmin)]
        [HttpPost("AddAdmin")]
        public async Task<ActionResult> AddAdmin(AdminDto admin)
        {
            if (admin != null)
            {
                if (!_dbContext.Admins.Any( _=> _.Username == admin.Username))
                {
                 var addadmin = _mapper.Map<Admin>(admin);
                addadmin.Role = Role.Admin;
                await _service.AddAdmin(addadmin);
                return Ok();

                }
                else
                { return BadRequest("username already taken "); }    
                
            }
            else
            {
                return StatusCode(500, "Admin info cannot be empty");
            }
        }

        [Authorize(Role.SuperAdmin)]
        [HttpPost("BlockAdmin")]
        public async Task<ActionResult> BlockAdmin(int id)
        {
            if (_dbContext.Admins.Any(_=>_.Id == id))
            {
                await _service.BlockAdmin(id);
                return Ok();
            }
            else
            {
                return StatusCode(500, "Admin with this id does not exist");
            }
        }
    }
}
