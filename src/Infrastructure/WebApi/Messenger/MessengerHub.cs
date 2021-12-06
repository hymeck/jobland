using Jobland.Infrastructure.Api.Web.Messenger.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace Jobland.Infrastructure.Api.Web.Messenger;

public class MessengerHub : Hub<ISignalRHubTypedClient>
{
}
