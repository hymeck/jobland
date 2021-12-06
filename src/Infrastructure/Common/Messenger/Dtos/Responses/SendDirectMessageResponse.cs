namespace Jobland.Infrastructure.Common.Messenger.Dtos.Responses;

public sealed record SendDirectMessageResponse(long Id, string SenderId, string ReceiverId, string Text, DateTime Created);
