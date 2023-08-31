
using AutoMapper;
using Nest;
using Esafe_Team_Project.Models.Client.Request;
using Esafe_Team_Project.Models.Address;
using Esafe_Team_Project.Models.Client.Response;
using AuthenticateResponse = Esafe_Team_Project.Models.Client.Response.AuthenticateResponse;
using Esafe_Team_Project.Models;

namespace Esafe_Team_Project.Entities
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Client, ClientDisplayDto>().ReverseMap();
            CreateMap<ClientDto, Client>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<Client, AuthenticateResponse>().ReverseMap();
            CreateMap<Admin, AuthenticateResponse>().ReverseMap();
            CreateMap<Transfer, TransferResponse>().ReverseMap();
            CreateMap<CreditCard, CreditCardDto>().ReverseMap();
            CreateMap<Certificate, CertificateDto>().ReverseMap(); 
            CreateMap<Certificate, CertificateReqDto>().ReverseMap();
        }
    }
}
