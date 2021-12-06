using AutoMapper;
using Jobland.Infrastructure.Api.Web.Messenger;
using Jobland.Infrastructure.Api.Web.Messenger.Abstractions;
using Jobland.Infrastructure.Common.Messenger.Dtos;
using Jobland.Infrastructure.Common.Messenger.Dtos.Requests;
using Jobland.Infrastructure.Common.Messenger.Dtos.Responses;
using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Jobland.Infrastructure.Api.Web.Endpoints;

public sealed class MessengerEndpoints : ApiEndpointBase
{
    public const string SendDirectMessageRoot = "/send-message";

    private readonly IHubContext<MessengerHub, ISignalRHubTypedClient> _messengerContext;
    private readonly IMapper _mapper;

    public MessengerEndpoints(IHubContext<MessengerHub, ISignalRHubTypedClient> messengerContext, IMapper mapper)
    {
        _messengerContext = messengerContext;
        _mapper = mapper;
    }

    [HttpPost(SendDirectMessageRoot)]
    public async Task<IActionResult> SendDirectMessage([FromQuery] string receiverId, [FromBody] MessageTextDto dto, CancellationToken token)
    {
        var request = new SendDirectMessageRequest(CurrentUserId, receiverId, dto.Text);
        var responseOption = await Sender.Send(request, token);

        await TrySendMessageAsync(receiverId, responseOption);
        
        return responseOption.Match(r => (IActionResult)Ok(r), BadRequest);
    }

    private async Task TrySendMessageAsync(string receiverId, Option<SendDirectMessageResponse> responseOption)
    {
        try
        {
            if (responseOption.IsSome)
                await _messengerContext.Clients.User(receiverId) // todo: map connectionId to receiverId
                    .SendDirectMessageAsync(_mapper.Map<SendDirectMessageResponse, DirectMessageDto>(responseOption.ValueUnsafe()));
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
