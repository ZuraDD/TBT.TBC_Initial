using System;
using FluentValidation;

namespace Application.PersonController.Commands.UpdatePerson
{
    public class UpdatePersonValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0);

            RuleFor(v => v.FirstName).NotEmpty().Matches("^([a-zA-Z]+|[ა-ჰ]+)$").MinimumLength(2).MaximumLength(50);

            RuleFor(v => v.LastName).NotEmpty().Matches("^([a-zA-Z]+|[ა-ჰ]+)$").MinimumLength(2).MaximumLength(50);

            RuleFor(v => v.CityId).NotEmpty().GreaterThan(0);

            RuleFor(v => v.PersonalNumber).NotEmpty().Length(11).Matches("^([0-9]+)$");

            RuleFor(v => v.BirthDate).Must(BeAValidDate).WithMessage("Age must be grater than 18").NotEmpty();

            RuleFor(v => v.GenderType).IsInEnum();

            RuleForEach(x => x.PhoneNumbers).ChildRules(phoneNumber =>
            {
                phoneNumber.RuleFor(v => v.TypeId).IsInEnum();
                phoneNumber.RuleFor(v => v.Value).MinimumLength(4).MaximumLength(50).Matches("^([0-9]+)$");
            });
        }

        private static bool BeAValidDate(DateTime bd)
        {
            return bd <= DateTime.UtcNow.AddYears(-18);
        }
    }
}
