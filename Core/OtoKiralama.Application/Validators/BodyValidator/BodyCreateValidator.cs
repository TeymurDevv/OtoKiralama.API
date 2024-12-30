using FluentValidation;
using OtoKiralama.Application.Dtos.Body;

namespace OtoKiralama.Application.Validators.BodyValidator
{
    public class BodyCreateValidator: AbstractValidator<BodyCreateDto>
    {
        public BodyCreateValidator()
        {
            RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("İsim alanı boş olamaz.")
            .MinimumLength(2)
            .WithMessage("isim metni en az 2 karakter uzunluğunda olmak zorunda.");
        }
    }
}
