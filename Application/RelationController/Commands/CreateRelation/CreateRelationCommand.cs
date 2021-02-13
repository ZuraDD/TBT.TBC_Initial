using Domain.Enums;
using MediatR;

namespace Application.RelationController.Commands.CreateRelation
{
    public class CreateRelationCommand : IRequest
    {
        public int PersonFor { get; set; }

        public int PersonTo { get; set; }

        public RelationTypeEnum RelationType { get; set; }
    }
}
