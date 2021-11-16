using AutoMapper;
using Domain;

namespace Application.Common.Works
{
    public class WorkMappingProfile : Profile
    {
        public WorkMappingProfile()
        {
            CreateMap<Work?, WorkDto?>()
                .ForMember(d => d.WorkId, map => map.MapFrom(s => s.Id));
            
        }
    }
}
