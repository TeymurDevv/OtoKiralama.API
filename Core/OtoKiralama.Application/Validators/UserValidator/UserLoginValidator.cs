using FluentValidation;
using OtoKiralama.Application.Dtos.User;

namespace OtoKiralama.Application.Validators.UserValidator
{
    public class UserLoginValidator : AbstractValidator<LoginDto>
    {
        public UserLoginValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı alanı zorunludur.")
                .MaximumLength(30).WithMessage("Kullanıcı adı en fazla 30 karakter olabilir.");
            RuleFor(r => r.Password)
                .MaximumLength(15).WithMessage("Şifre en fazla 15 karakter olabilir.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
        }
    }
}
