using FluentValidation;

namespace Application.PersonController.Commands.DeletePerson
{
    public class DeletePersonValidator : AbstractValidator<DeletePersonCommand>
    {
        public DeletePersonValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0);
        }

    }
}
