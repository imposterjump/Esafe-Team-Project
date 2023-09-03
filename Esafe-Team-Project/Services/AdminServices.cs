using AutoMapper;
using Esafe_Team_Project.Data;
using Esafe_Team_Project.Entities;
using Esafe_Team_Project.Helpers;
using Esafe_Team_Project.Models.Client.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text;

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
        public async Task<(List<Certificate>,string)> Get_All_Cert_Req()
        {

            var certificates = await dbContext.Certificates.Where(client=> client.Accepted==false).ToListAsync();
            if(certificates.Count > 0)
            {
                return (certificates,"success");
            }
            else
            {
                return (null, "there is no request till now you can rest admin");
            }
           
            
            
        }
        public string Generate4Digits()
        {

            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                int digit = random.Next(0, 10);
                sb.Append(digit);
            }
            return sb.ToString();
        }

        public string Generate16Digits()
        {

            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                int digit = random.Next(0, 10);
                sb.Append(digit);
            }
            return sb.ToString();
        }


        public bool compareCVV(string CVV)
        {
            var CVVs = dbContext.CreditCards.Select(_ => _.CVV).ToList();
            foreach (var cvv in CVVs)
            {
                if (cvv == CVV)
                {
                    return true;
                }
            }
            return false;
        }

        public bool compareCardNumber(string CardNumber)
        {
            var CardNos = dbContext.CreditCards.Select(_ => _.CardNumber).ToList();
            foreach (var cardno in CardNos)
            {
                if (cardno == CardNumber)
                {
                    return true;
                }
            }
            return false;
        }




        public async Task<ActionResult<(string, CreditCard)>> approveCreditCard(int CreditCardId, int admin_id)
        {
            var card = await dbContext.CreditCards.FindAsync(CreditCardId);
            if (card == null || admin_id == null)
            {
                return ("admin id or certificate id id invalid", null);
            }
            else
            {
                string CVV;
                string CardNumber;
                DateTime now = DateTime.Now;
                do
                {
                    CVV = Generate4Digits();
                }
                while (compareCVV(CVV));

                do
                {
                    CardNumber = Generate16Digits();
                }
                while (compareCardNumber(CardNumber));

                var client = await dbContext.Clients.FindAsync(card.ClientId);
                card.Accepted = true;
                card.AcceptanceDate = DateTime.Now;
                card.ApprovedById = admin_id;
                card.CardNumber = CardNumber;
                card.CVV = CVV;
                card.ExpiryDate = now.AddYears(1);
                card.BalanceAvailable = client.balance;
                if (card.CardType == "Silver")
                    card.CardLimit = 10000;
                else if (card.CardType == "Gold")
                    card.CardLimit = 20000;
                else
                    card.CardLimit = 30000;
                dbContext.Entry(card).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return ("creditCard approved ", card);


            }
        }

    }
}
