using LanguageExt;

namespace Jobland.Infrastructure.Common.Messenger.Abstractions;

public interface IDirectMessageService
{
    public Task<Option<DirectMessage>> SendDirectMessageAsync(string senderId, string receiverId, string text, CancellationToken token = default);
}
