using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.RelationController.Commands.DeleteRelation
{
    public class DeleteRelationCommand : IRequest
    {
        [FromRoute(Name = "personId")]
        public int PersonId { get; set; }

        [FromRoute(Name = "relationId")]
        public int RelationId { get; set; }
    }
}
