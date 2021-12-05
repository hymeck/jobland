using Jobland.Infrastructure.Common.Identity.Dtos.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobland.Infrastructure.Api.Web.Endpoints;

public sealed class UserProfileEndpoints : ApiEndpointBase
{
    [HttpPost("/upload-image")]
    public async Task<IActionResult> UploadProfileImage([FromForm] IFormFile image, CancellationToken token)
    {
        var request = new UploadProfileImageRequest(image, CurrentUserId);
        var response = await Sender.Send(request, token);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [AllowAnonymous]
    [HttpGet("/profile")]
    public async Task<IActionResult> GetUserProfile([FromQuery] GetUserProfileRequest request, CancellationToken token)
    {
        var response = await Sender.Send(request, token);
        return response.Match(up => (IActionResult)Ok(up), NotFound);
    }
}
