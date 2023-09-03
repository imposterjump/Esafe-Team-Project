using AutoMapper;
using Esafe_Team_Project.Data;
using Esafe_Team_Project.Helpers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quartz;

namespace Esafe_Team_Project.Jobs
{
    public class ClientUpdateBalanceJob:IJob
    {
        private readonly AppDbContext _dbContext;
     
        public ClientUpdateBalanceJob(AppDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        
        
       

        public async Task Execute(IJobExecutionContext context)
        {
            var result = await _dbContext.Clients.ToListAsync();
            for (int i=0;i<result.Count; i++)
            {
                result[i].balance += 10;

            }
            _dbContext.UpdateRange(result);
            await  _dbContext.SaveChangesAsync();
        }
    }
}
