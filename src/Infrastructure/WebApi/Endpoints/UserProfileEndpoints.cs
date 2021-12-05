using Jobland.Infrastructure.Common.Identity.Dtos.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobland.Infrastructure.Api.Web.Endpoints;

public sealed class UserProfileEndpoints : ApiEndpointBase
{
    public const string ProfileRoot = "/profile";
    public const string UploadProfileImageRoot = ProfileRoot + "/upload-image";

    [HttpPost(UploadProfileImageRoot)]
    public async Task<IActionResult> UploadProfileImage([FromForm] IFormFile image, CancellationToken token)
    {
        var request = new UploadProfileImageRequest(image, CurrentUserId);
        var response = await Sender.Send(request, token);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [AllowAnonymous]
    [HttpGet(ProfileRoot)]
    public async Task<IActionResult> GetUserProfile([FromQuery] GetUserProfileRequest request, CancellationToken token)
    {
        var response = await Sender.Send(request, token);
        return response.Match(up => (IActionResult)Ok(up), NotFound);
    }
    
    [HttpPut(ProfileRoot)]
    public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileRequest request, CancellationToken token)
    {
        request.UserId = CurrentUserId;
        var response = await Sender.Send(request, token);
        return response.Succeeded ? NoContent() : BadRequest(response);
    }
}
