
using AutoMapper;
using Nest;
using Esafe_Team_Project.Models.Client.Request;
using Esafe_Team_Project.Models.Address;
using Esafe_Team_Project.Models.Client.Response;
using AuthenticateResponse = Esafe_Team_Project.Models.Client.Response.AuthenticateResponse;

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
        }
    }
}
