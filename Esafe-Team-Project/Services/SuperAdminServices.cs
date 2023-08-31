using AutoMapper;
using Esafe_Team_Project.Data;
using Esafe_Team_Project.Data.Enums;
using Esafe_Team_Project.Entities;
using Esafe_Team_Project.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Esafe_Team_Project.Services
{
    public class SuperAdminServices
    {
        private readonly AppDbContext _dbContext;
        private readonly ClientService _clientservice;
        public SuperAdminServices(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddAdmin(Admin admin)
        {
            admin.Role = Role.Admin;
            admin.Password = _clientservice.HashPassword(admin.Password);
            try
            {
                await _dbContext.Admins.AddAsync(admin);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine("admin added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task BlockAdmin(int id)
        {
            try
            {
                Console.WriteLine("inside blocking");
                var editadmin = _dbContext.Admins.Find(id);
                editadmin.Blocked = true;
                _dbContext.Entry(editadmin).State = EntityState.Modified;
                _dbContext.SaveChanges();
                Console.WriteLine(editadmin.Blocked);
                Console.WriteLine("admin blocked successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}