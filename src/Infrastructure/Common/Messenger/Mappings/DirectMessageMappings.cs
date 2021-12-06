using AutoMapper;
using Jobland.Infrastructure.Common.Messenger.Dtos.Responses;

namespace Jobland.Infrastructure.Common.Messenger.Mappings;

public sealed class DirectMessageMappings : Profile
{
    public DirectMessageMappings()
    {
        CreateMap<DirectMessage, SendDirectMessageResponse>();
    }
}
