using AutoMapper;
using Domain;

namespace Application.Commands.Works
{
    public sealed class WorkMappingProfile : Profile
    {
        public WorkMappingProfile()
        {
            CreateMap<CreateWorkCommand, Work>();
        }
    }
}
