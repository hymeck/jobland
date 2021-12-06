using AutoMapper;
using Jobland.Infrastructure.Common.Messenger.Abstractions;
using Jobland.Infrastructure.Common.Messenger.Dtos.Requests;
using Jobland.Infrastructure.Common.Messenger.Dtos.Responses;
using LanguageExt;
using MediatR;

namespace Jobland.Infrastructure.Common.Messenger.Commands;

public sealed class SendDirectMessageRequestHandler : IRequestHandler<SendDirectMessageRequest, Option<SendDirectMessageResponse>>
{
    private readonly IDirectMessageService _messageService;
    private readonly IMapper _mapper;

    public SendDirectMessageRequestHandler(IDirectMessageService messageService, IMapper mapper)
    {
        _messageService = messageService;
        _mapper = mapper;
    }

    public async Task<Option<SendDirectMessageResponse>> Handle(SendDirectMessageRequest request, CancellationToken cancellationToken)
    {
        var result = await _messageService.SendDirectMessageAsync(request.SenderId, request.ReceiverId, request.Text, cancellationToken);
        return result.Map(m => _mapper.Map<DirectMessage, SendDirectMessageResponse>(m));
    }
}
