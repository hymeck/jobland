using Microsoft.AspNetCore.SignalR;

namespace Jobland.Infrastructure.Api.Web.Messenger.Implementations;

public class JoblandUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection) => 
        connection.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
}
