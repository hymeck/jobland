using AutoMapper;
using Jobland.Infrastructure.Common.Identity.Dtos.Requests;

namespace Jobland.Infrastructure.Common.Identity.Mappings;

public class UserMappings : Profile
{
    public UserMappings()
    {
        CreateMap<RegistrationRequest, User>()
            .ForMember(d => d.Gender, s => s.MapFrom(r => r.Gender ?? Gender.Male))
            .ForMember(d => d.LastName, s => s.MapFrom(r => r.LastName ?? ""))
            .ForMember(d => d.PhoneNumber, s => s.MapFrom(r => r.PhoneNumber ?? ""));
    }
}
