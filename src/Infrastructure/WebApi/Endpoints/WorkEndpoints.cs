using Jobland.Application.Logic.Works.Dtos.Requests;
using Jobland.Infrastructure.Common.Logic.Works.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Jobland.Infrastructure.Api.Web.Endpoints;

public sealed class WorkEndpoints : ApiEndpointBase
{
    public const string WorkRoot = "/works";
    public const string WorkIdRoot = WorkRoot + "/{id}";
    public const string WorkTitleRoot = WorkRoot + "-title";
    public const string WorkCountRoot = WorkRoot + "/count";
    public const string WorkFilterRoot = WorkRoot + "/filter";
    public const string RespondWorkRoot = WorkRoot + "/respond";
    public const string UserWorksRoot = "user-works";
    
    [HttpPost(WorkRoot)]
    public async Task<IActionResult> AddWork([FromBody] AddWorkRequest request, CancellationToken token)
    {
        var response = await Sender.Send(request, token);
        return response.Match(w => (IActionResult)Ok(w), BadRequest);
    }
    
    [HttpGet(WorkIdRoot)]
    public async Task<IActionResult> GetWorkById(long id, CancellationToken token)
    {
        var request = new GetWorkByIdRequest(id);
        var response = await Sender.Send(request, token);
        return response.Match(w => (IActionResult)Ok(w), NotFound);
    }
    
    [HttpGet(WorkTitleRoot)]
    public async Task<IActionResult> GetWorksByTitle([FromQuery] GetWorksByTitleRequest request, CancellationToken token) => 
        Ok(await Sender.Send(request, token));

    [HttpGet(WorkRoot)]
    public async Task<IActionResult> GetWorks([FromQuery] GetWorksRequest request, CancellationToken token) => 
        Ok(await Sender.Send(request, token));

    [HttpGet(WorkCountRoot)]
    public async Task<IActionResult> GetWorkCount(CancellationToken token) => Ok(await Sender.Send(new GetWorkCountRequest(), token));

    [HttpGet(WorkFilterRoot)]
    public async Task<IActionResult> GetWorksByFilter([FromQuery] GetWorksByFilterRequest request,
        CancellationToken token) =>
        Ok(await Sender.Send(request, token));

    [HttpPost(RespondWorkRoot)]
    public async Task<IActionResult> RespondWork([FromQuery] RespondWorkRequest request, CancellationToken token) => 
        await Sender.Send(request, token) ? Ok() : BadRequest();

    [HttpPut(WorkRoot)]
    public async Task<IActionResult> EditWork([FromBody] UpdateWorkRequest request, CancellationToken token) => 
        await Sender.Send(request, token) ? Ok() : BadRequest();

#if DEBUG
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
#endif
    [HttpGet(UserWorksRoot)]
    public async Task<IActionResult> GetUserAddedWorks([FromQuery] GetUserAddedWorksRequest request, CancellationToken token)
    {
        var responseOption = await Sender.Send(request, token);
        return responseOption.Match(r => (IActionResult)Ok(r), NotFound);
    }
}
