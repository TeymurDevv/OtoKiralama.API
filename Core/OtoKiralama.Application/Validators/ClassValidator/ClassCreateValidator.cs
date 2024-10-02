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
                .WithMessage("Name is required.")
                .MinimumLength(3)
                .WithMessage("Name must be at least 3 characters long");
        }
    }
}
