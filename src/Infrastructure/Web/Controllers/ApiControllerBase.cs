using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private ISender _sender;

        protected ISender RequestSender => _sender ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
