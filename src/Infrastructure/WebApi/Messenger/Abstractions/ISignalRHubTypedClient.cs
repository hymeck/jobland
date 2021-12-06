namespace Jobland.Infrastructure.Api.Web.Messenger.Abstractions;

public interface ISignalRHubTypedClient
{
    public Task SendDirectMessageAsync(DirectMessageDto message);
}
