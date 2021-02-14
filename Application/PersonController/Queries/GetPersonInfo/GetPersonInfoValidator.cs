using FluentValidation;

namespace Application.PersonController.Queries.GetPersonInfo
{
    public class GetPersonInfoValidator : AbstractValidator<GetPersonInfoQuery>
    {
        public GetPersonInfoValidator()
        {
            RuleFor(x => x.PersonId).NotEmpty().GreaterThan(0);
        }
    }
}
