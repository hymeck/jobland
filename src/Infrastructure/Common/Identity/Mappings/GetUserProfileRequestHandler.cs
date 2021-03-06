using AutoMapper;
using Jobland.Infrastructure.Common.Identity.Dtos.Requests;
using Jobland.Infrastructure.Common.Identity.Dtos.Responses;
using Jobland.Infrastructure.Common.Persistence;
using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Jobland.Infrastructure.Common.Identity.Mappings;

public sealed class GetUserProfileRequestHandler : IRequestHandler<GetUserProfileRequest, Option<GetUserProfileResponse>>
{
    private readonly UserManager<User> _manager;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _db;
    public string DefaultImageUrl => "https://joblandmedia.blob.core.windows.net/profileimages/default-user-profile-image.svg";

    public GetUserProfileRequestHandler(UserManager<User> manager, IMapper mapper, ApplicationDbContext db)
    {
        _manager = manager;
        _mapper = mapper;
        _db = db;
    }

    public async Task<Option<GetUserProfileResponse>> Handle(GetUserProfileRequest request, CancellationToken cancellationToken)
    {
        var userOption = (Option<User>)await _manager.FindByIdAsync(request.UserId);
        return userOption.IsNone 
            ? Option<GetUserProfileResponse>.None 
            : await userOption.BindAsync(ToResponse);
    }

    private async Task<OptionAsync<GetUserProfileResponse>> ToResponse(User user)
    {
        var response = _mapper.Map<User, GetUserProfileResponse>(user);
        var images = await _db.ProfileImages
            .Where(pi => pi.OwnerId == user.Id)
            .Select(pi => pi.ImageUrl)
            .ToListAsync();

        response.Images = images switch
        {
            null => new List<string> { DefaultImageUrl },
            { Count: 0 } => new List<string> { DefaultImageUrl },
            _ => images
        };
        return response;
    }
}
