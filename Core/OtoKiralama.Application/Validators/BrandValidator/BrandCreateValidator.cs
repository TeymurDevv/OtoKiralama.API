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
                .WithMessage("İsim alanı boş olamaz.")
                .MinimumLength(3)
                .WithMessage("isim metni en az 3 karakter uzunluğunda olmak zorunda.");
        }
    }
}
