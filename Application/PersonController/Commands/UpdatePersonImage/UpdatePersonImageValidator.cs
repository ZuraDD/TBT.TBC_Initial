using FluentValidation;

namespace Application.PersonController.Commands.UpdatePersonImage
{
    public class UpdatePersonImageValidator : AbstractValidator<UpdatePersonImageCommand>
    {
        public UpdatePersonImageValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0);

            RuleFor(v => v.ProfileImage).NotEmpty();

            RuleFor(v => v.ProfileImage.Length).NotEmpty().LessThanOrEqualTo(50 * 1000000);

            RuleFor(v => v.ProfileImage.ContentType).NotEmpty().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"));
        }

    }
}
