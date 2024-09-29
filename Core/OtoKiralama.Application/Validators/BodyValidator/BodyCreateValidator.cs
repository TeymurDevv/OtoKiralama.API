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
            .WithMessage("Name is required.")
            .MinimumLength(3)
            .WithMessage("Name must be at least 3 characters long");
        }
    }
}
