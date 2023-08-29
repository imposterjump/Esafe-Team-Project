using AutoMapper;
using Esafe_Team_Project.Data;
using Esafe_Team_Project.Entities;
using Esafe_Team_Project.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Esafe_Team_Project.Services
{
    public class AdminServices
    {
        private readonly Jwt _jwt;
        private readonly AppDbContext dbContext;
        private readonly IMapper _mapper;
        public AdminServices(AppDbContext dbContext, IMapper mapper, IOptions<Jwt> optionsJwt)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
            _jwt = optionsJwt.Value;
        }
        public async Task<List<Transfer>> AdminGetTransferInfo()
        {
            List<Transfer> transfers = await dbContext.Transfers.ToListAsync();
            if (transfers != null)
            {
                return transfers;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Transfer>> AdminGetTransferInfoById(int id)
        {
            List<Transfer> transfers = await dbContext.Transfers.Where(client => client.RecieverId == id).ToListAsync();
            if (transfers != null)
            {
                return transfers;
            }
            else
            {
                return null;
            }
        }
    }
}
