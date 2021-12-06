namespace Jobland.Infrastructure.Api.Web.Messenger;

public sealed record DirectMessageDto(long Id, string SenderId, string ReceiverId, string Text, DateTime Created)
{
    public bool Your => SenderId == ReceiverId;
}
