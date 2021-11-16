using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Works;
using Application.Queries.Works;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/works")]
    public class WorksController : ApiControllerBase
    {
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetWork(long id, CancellationToken token)
        {
            var query = new GetWorkByIdQuery(id);
            var response = await RequestSender.Send(query, token);
            return response.Success ? Ok(response) : NotFound(response);
        }
        
        public async Task<IActionResult> GetWork([FromQuery] string name, CancellationToken token)
        {
            var query = new GetWorkByNameQuery(name);
            var response = await RequestSender.Send(query, token);
            return response.Success ? Ok(response) : NotFound(response);
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> AddWork([FromBody] CreateWorkCommand command, CancellationToken token)
        {
            var response = await RequestSender.Send(command, token);
            return response.Success
                ? CreatedAtAction("GetWork", new {id = response.Item.WorkId}, response)
                : BadRequest();
        }
    }
}
