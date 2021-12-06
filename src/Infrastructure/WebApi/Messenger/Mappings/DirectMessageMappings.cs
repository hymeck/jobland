using AutoMapper;
using Jobland.Infrastructure.Common.Messenger.Dtos.Responses;

namespace Jobland.Infrastructure.Api.Web.Messenger.Mappings;

public class DirectMessageMappings : Profile
{
    public DirectMessageMappings()
    {
        CreateMap<SendDirectMessageResponse, DirectMessageDto>();
    }
}
