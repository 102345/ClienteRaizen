using AutoMapper;
using Raizen.Cliente.Application.Contracts;

namespace Raizen.Cliente.Presentation.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteModel, Domain.Entities.Cliente>();
            CreateMap<Domain.Entities.Cliente, ClienteModel>();
        }
    }
}
