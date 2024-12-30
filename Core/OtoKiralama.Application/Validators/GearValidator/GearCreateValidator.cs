using FluentValidation;
using OtoKiralama.Application.Dtos.Gear;

namespace OtoKiralama.Application.Validators.GearValidator
{
    public class GearCreateValidator : AbstractValidator<GearCreateDto>
    {
        public GearCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı zorunludur.");
            RuleFor(x => x.Name).MaximumLength(15).WithMessage("Ad 15 karakterden uzun olamaz.");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Ad 3 karakterden kısa olamaz.");
        }
    }
}
