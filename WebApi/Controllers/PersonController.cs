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
    public class PersonController : BaseController
    {
        public PersonController(IMediator mediator) : base(mediator) { }

        [HttpGet("person/{id}")]
        public async Task<GetPersonInfoDto> GetPersonInfo([FromRoute] int id, [FromQuery] GetPersonInfoQuery command)
        {
            command.PersonId = id;

            return await Mediator.Send(command);
        }

        [HttpGet("persons")]
        public async Task<PaginatedList<GetPersonListDto>> GetPersonList([FromQuery] GetPersonListQuery command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("person")]
        public async Task<Unit> Create([FromBody] CreatePersonCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("person/{id}")]
        public async Task<Unit> Delete([FromRoute] int id)
        {
            return await Mediator.Send(new DeletePersonCommand{Id = id});
        }

        [HttpPut("person/{id}")]
        public async Task<Unit> Update([FromRoute] int id, [FromBody] UpdatePersonCommand command)
        {
            command.Id = id;

            return await Mediator.Send(command);
        }

        [HttpPut("person/{id}/image")]
        public async Task<Unit> UpdateImage([FromRoute] int id, [FromForm] UpdatePersonImageCommand command)
        {
            command.Id = id;

            return await Mediator.Send(command);
        }
    }
}
