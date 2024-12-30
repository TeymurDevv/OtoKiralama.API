using FluentValidation;
using OtoKiralama.Application.Dtos.Role;

namespace OtoKiralama.Application.Validators.RoleValidator
{
    public class RoleCreateValidator : AbstractValidator<RoleCreateDto>
    {
        public RoleCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı zorunludur.");
        }
    }
}
