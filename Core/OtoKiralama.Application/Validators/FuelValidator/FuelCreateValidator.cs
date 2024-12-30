using FluentValidation;
using OtoKiralama.Application.Dtos.Fuel;

namespace OtoKiralama.Application.Validators.FuelValidator
{
    public class FuelCreateValidator : AbstractValidator<FuelCreateDto>
    {
        public FuelCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı zorunludur.");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Ad 50 karakterden uzun olamaz.");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Ad 3 karakterden kısa olamaz.");
        }
    }
}
