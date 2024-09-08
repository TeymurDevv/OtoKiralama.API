using FluentValidation;
using OtoKiralama.Application.Dtos.User;

namespace OtoKiralama.Application.Validators.UserValidator
{
    public class UserLoginValidator : AbstractValidator<LoginDto>
    {
        public UserLoginValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(r => r.Password)
                .MaximumLength(15)
                .MinimumLength(6);
        }
    }
}
