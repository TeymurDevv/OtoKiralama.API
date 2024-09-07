using FluentValidation;
using OtoKiralama.Application.Dtos.User;

namespace OtoKiralama.Application.Validators.UserValidator
{
    public class UserRegisterValidator : AbstractValidator<RegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(r => r.FullName)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(r => r.UserName)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(r => r.Password)
                .NotEmpty()
                .MaximumLength(15)
                .MinimumLength(6);
            RuleFor(r => r.RePassword)
                .Equal(r => r.Password);
        }
    }
}
