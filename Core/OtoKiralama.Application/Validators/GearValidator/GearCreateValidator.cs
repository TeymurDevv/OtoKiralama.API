using FluentValidation;
using OtoKiralama.Application.Dtos.Gear;

namespace OtoKiralama.Application.Validators.GearValidator
{
    public class GearCreateValidator : AbstractValidator<GearCreateDto>
    {
        public GearCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Name).MaximumLength(15).WithMessage("Name can't be more than 50 character");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Name can't be less than 3 character");
        }
    }
}
