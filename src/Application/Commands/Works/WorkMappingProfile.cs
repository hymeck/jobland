using AutoMapper;
using Domain;

namespace Application.Commands.Works
{
    public sealed class WorkMappingProfile : Profile
    {
        public WorkMappingProfile()
        {
            CreateMap<CreateWorkCommand, Work>();

            CreateMap<Work, CreateWorkCommandResponse>()
                .ForMember(d => d.WorkId, map => map.MapFrom(s => s.Id));
        }
    }
}
