using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Mappings;
using Application.PersonController.Commands.CreatePerson;
using MediatR;
using Application.PersonController.Commands.DeletePerson;
using Application.PersonController.Commands.UpdatePerson;
using Application.PersonController.Commands.UpdatePersonImage;
using Application.PersonController.Queries.GetPersonInfo;
using Application.PersonController.Queries.GetPersonInfo.Models;
using Application.PersonController.Queries.GetPersonList;
using Application.PersonController.Queries.GetPersonList.Models;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("persons")]
    public class PersonController : BaseController
    {
        public PersonController(IMediator mediator) : base(mediator) { }

        [HttpGet("{personId}")]
        public async Task<GetPersonInfoDto> GetPersonInfo([FromRoute] int personId, [FromQuery] GetPersonInfoQuery command)
        {
            command.PersonId = personId;

            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginatedList<GetPersonListDto>> GetPersonList([FromQuery] GetPersonListQuery command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        public async Task<Unit> Create([FromBody] CreatePersonCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{personId}")]
        public async Task<Unit> Delete([FromRoute] DeletePersonCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{personId}")]
        public async Task<Unit> Update([FromRoute] int personId, [FromBody] UpdatePersonCommand command)
        {
            command.Id = personId;

            return await Mediator.Send(command);
        }

        [HttpPut("Image")]
        public async Task<Unit> UpdateImage([FromRoute] int personId, [FromForm] UpdatePersonImageCommand command)
        {
            command.Id = personId;

            return await Mediator.Send(command);
        }
    }
}
