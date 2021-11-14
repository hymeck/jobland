using AutoMapper;
using Domain;

namespace Application.Queries.Works
{
    public class WorkMappingProfile : Profile
    {
        public WorkMappingProfile()
        {
            CreateMap<Work, WorkDto>();
            CreateMap<Work?, WorkDto?>();
        }
    }
}
