using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Application.RelationController.Commands.CreateRelation;
using Application.RelationController.Commands.DeleteRelation;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class RelationController : BaseController
    {
        public RelationController(IMediator mediator) : base(mediator) { }

        [HttpPost("person/{id}/relation")]
        public async Task<Unit> Create([FromRoute] int id, [FromBody] CreateRelationCommand command)
        {
            command.PersonFor = id;

            return await Mediator.Send(command);
        }

        [HttpDelete("person/{personId}/relation/{relationId}")]
        public async Task<Unit> Delete([FromRoute] DeleteRelationCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
