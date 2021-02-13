using FluentValidation;

namespace Application.RelationController.Commands.DeleteRelation
{
    public class DeleteRelationValidator : AbstractValidator<DeleteRelationCommand>
    {
        public DeleteRelationValidator()
        {
            RuleFor(v => v.PersonId).NotEmpty().GreaterThan(0);

            RuleFor(v => v.RelationId).NotEmpty().GreaterThan(0);
        }

    }
}
