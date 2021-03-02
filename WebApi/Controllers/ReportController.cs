using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Application.ReportController.Queries.GetReport;
using Application.ReportController.Queries.GetReport.Models;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("reports")]
    public class ReportController : BaseController
    {
        public ReportController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<List<GetReportDto>> Get([FromQuery] GetReportQuery command)
        {
            return await Mediator.Send(command);
        }
    }
}
