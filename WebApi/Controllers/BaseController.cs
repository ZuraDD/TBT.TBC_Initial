using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;

        public BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
