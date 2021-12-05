using Jobland.Infrastructure.Common.Identity.Dtos.Requests;
using Jobland.Infrastructure.Common.Identity.Dtos.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Jobland.Infrastructure.Common.Identity.Commands;

public sealed class UpdateUserProfileRequestHandler : IRequestHandler<UpdateUserProfileRequest, UpdateUserProfileResponse>
{
    private readonly UserManager<User> _manager;

    public UpdateUserProfileRequestHandler(UserManager<User> manager) => _manager = manager;

    public async Task<UpdateUserProfileResponse> Handle(UpdateUserProfileRequest request, CancellationToken cancellationToken)
    {
        var user = await _manager.FindByIdAsync(request.UserId);
        if (user == null)
            return new UpdateUserProfileResponse(false, new List<string> { "account with specified email does not exist" });
        
        if (!string.IsNullOrEmpty(request.Password))
        {
            var newPasswordHash = _manager.PasswordHasher.HashPassword(user, request.Password);
            if (newPasswordHash != null)
                user.PasswordHash = newPasswordHash;
        }
        if (!string.IsNullOrEmpty(request.Email))
            user.Email = request.Email;
        if (!string.IsNullOrEmpty(request.FirstName))
            user.FirstName = request.FirstName;
        if (!string.IsNullOrEmpty(request.LastName))
            user.LastName = request.LastName;
        if (request.BirthDate.HasValue)
            user.BirthDate = request.BirthDate.GetValueOrDefault();
        if (request.Gender.HasValue)
            user.Gender = request.Gender.GetValueOrDefault();
        
        var updateProfileResult = await _manager.UpdateAsync(user);
        return FromIdentityResult(updateProfileResult);
    }

    private UpdateUserProfileResponse FromIdentityResult(IdentityResult result) =>
        new(result.Succeeded, result.Errors.Select(e => e.Description).ToList());
}
