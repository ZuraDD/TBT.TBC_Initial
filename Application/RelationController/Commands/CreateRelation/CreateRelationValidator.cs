using FluentValidation;

namespace Application.RelationController.Commands.CreateRelation
{
    public class CreateRelationValidator : AbstractValidator<CreateRelationCommand>
    {
        public CreateRelationValidator()
        {
            RuleFor(v => v.PersonFor).NotEmpty().GreaterThan(0);

            RuleFor(v => v.PersonFor).NotEmpty().GreaterThan(0);

            RuleFor(v => v.RelationType).NotEmpty().IsInEnum();
        }
    }
}
