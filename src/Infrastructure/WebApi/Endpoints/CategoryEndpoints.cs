using Jobland.Application.Logic.Categories.Dtos.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobland.Infrastructure.Api.Web.Endpoints;

public sealed class CategoryEndpoints : ApiEndpointBase
{
    [AllowAnonymous]
    [HttpGet("/categories")]
    public async Task<IActionResult> GetCategories(CancellationToken token)
    {
        var request = new GetAllCategoriesRequest();
        var response = await Sender.Send(request, token);
        return Ok(response);
    }
}
