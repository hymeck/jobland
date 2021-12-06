using Jobland.Infrastructure.Common.Messenger.Dtos.Responses;
using LanguageExt;
using MediatR;

namespace Jobland.Infrastructure.Common.Messenger.Dtos.Requests;

public sealed record SendDirectMessageRequest(string SenderId, string ReceiverId, string Text) 
    : IRequest<Option<SendDirectMessageResponse>>;
