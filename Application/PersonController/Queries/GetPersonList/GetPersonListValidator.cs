using System;
using FluentValidation;

namespace Application.PersonController.Queries.GetPersonList
{
    public class GetPersonListValidator : AbstractValidator<GetPersonListQuery>
    {
        public GetPersonListValidator()
        {
            RuleFor(v => v.FirstName).Matches("^([a-zA-Z]+|[ა-ჰ]+)$").MinimumLength(2).MaximumLength(50);

            RuleFor(v => v.LastName).Matches("^([a-zA-Z]+|[ა-ჰ]+)$").MinimumLength(2).MaximumLength(50);

            RuleForEach(x => x.CityIds).ChildRules(cityId =>
            {
                cityId.RuleFor(v => v).NotEmpty().GreaterThan(0);
            });

            RuleFor(v => v.PersonalNumber).MaximumLength(11).Matches("^([0-9]+)$");

            RuleFor(v => v.BirthDateFrom).Must(BeAValidDate).WithMessage("Age must be grater than 18");

            RuleFor(v => v.BirthDateTo).Must(BeAValidDate).WithMessage("Age must be grater than 18");

            RuleFor(v => v.GenderTypeId).IsInEnum();

            RuleFor(v => v.PageNumber).NotEmpty().GreaterThan(0);

            RuleFor(v => v.PageSize).NotEmpty().GreaterThan(0);
        }

        private static bool BeAValidDate(DateTime? bd)
        {
            return !bd.HasValue || bd.Value <= DateTime.UtcNow.AddYears(-18);
        }
    }
}
