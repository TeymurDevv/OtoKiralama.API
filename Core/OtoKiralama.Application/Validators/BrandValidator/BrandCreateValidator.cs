using FluentValidation;
using OtoKiralama.Application.Dtos.Brand;

namespace OtoKiralama.Application.Validators.BrandValidator
{
    public class BrandCreateValidator : AbstractValidator<BrandCreateDto>
    {
        public BrandCreateValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(3)
                .WithMessage("Name must be at least 3 characters long");
        }
    }
}
