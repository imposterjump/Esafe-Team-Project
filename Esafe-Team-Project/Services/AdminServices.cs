using AutoMapper;
using Esafe_Team_Project.Data;
using Esafe_Team_Project.Entities;
using Esafe_Team_Project.Helpers;
using Microsoft.AspNetCore.Mvc;
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
            List<Transfer> transfers = await dbContext.Transfers.Where(client => client.SenderId == id|| client.RecieverId == id).ToListAsync();
            if (transfers != null)
            {
                return transfers;
            }
            else
            {
                return null;
            }
        }
        public async Task<ActionResult<(string, Certificate)>> approveCertificate(int CertificateId,int  admin_id)
        {
            var c1 = await dbContext.Certificates.FindAsync(CertificateId);
            if(c1 == null|| admin_id ==null)
            {
                return ("admin id or certificate id id invalid", null);
            }
            else
            {
                c1.Accepted = true;
                c1.AcceptanceDate = DateTime.Now;
                c1.ApprovedById = admin_id;
                dbContext.Entry(c1).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return ("certificate approved ", c1);

            }

        }
    }
}
