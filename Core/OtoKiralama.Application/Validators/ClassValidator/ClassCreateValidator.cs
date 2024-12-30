using FluentValidation;
using OtoKiralama.Application.Dtos.Class;

namespace OtoKiralama.Application.Validators.ClassValidator
{
    public class ClassCreateValidator:AbstractValidator<ClassCreateDto>
    {
        public ClassCreateValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("İsim alanı zorunludur.")
                .MinimumLength(3)
                .WithMessage("isim alanı en az 3 karakter uzunluğunda olmak zorunda.");
        }
    }
}
