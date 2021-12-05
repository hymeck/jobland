using Jobland.Domain.Common;
using Jobland.Infrastructure.Common.Identity;

namespace Jobland.Infrastructure.Common.Messenger;

/// <summary>
/// represents message being sent from one user to other user directly.
/// </summary>
public sealed class DirectMessage : AuditableEntity
{
    public string TextBody { get; set; } = ""; 
    public string? SenderId { get; set; } = "";
    public User? Sender { get; set; }
    public string? ReceiverId { get; set; } = "";
    public User? Receiver { get; set; }
}
