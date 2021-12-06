using Jobland.Infrastructure.Common.Identity;
using Jobland.Infrastructure.Common.Messenger;
using Jobland.Infrastructure.Common.Messenger.Abstractions;
using Jobland.Infrastructure.Common.Persistence;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Jobland.Infrastructure.Api.Web.Messenger.Implementations;

public sealed class DirectMessageService : IDirectMessageService
{
    private readonly ApplicationDbContext _db;

    public DirectMessageService(ApplicationDbContext db) => _db = db;

    public async Task<Option<DirectMessage>> SendDirectMessageAsync(string senderId, string receiverId, string text, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(text))
            return null!;
        
        var sender = await FindByIdAsync(senderId);
        if (sender == null)
            return null!;

        if (senderId == receiverId)
            return await StoreMessageAsync(sender, sender, text, token);

        var receiver = await FindByIdAsync(receiverId);
        if (receiver == null)
            return null!;

        return await StoreMessageAsync(sender, receiver, text, token);
    }

    private async Task<User?> FindByIdAsync(string id) => await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

    private async Task<Option<DirectMessage>> StoreMessageAsync(User sender, User receiver, string text, CancellationToken token)
    {
        var message = new DirectMessage { Sender = sender, Receiver = receiver, TextBody = text };
        _db.DirectMessages.Add(message);
        var added = await _db.SaveChangesSafelyAsync(token);
        return added.Map(_ => message);
    }
}
