using AutoMapper;

namespace Jobland.Authentication;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegistrationRequest, User>();
    }
}
