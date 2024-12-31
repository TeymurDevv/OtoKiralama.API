using FluentValidation;
using OtoKiralama.Application.Dtos.User;

namespace OtoKiralama.Application.Validators.UserValidator
{
    public class UserRegisterValidator : AbstractValidator<RegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(r => r.FullName)
                .NotEmpty().WithMessage("Ad ve soyad alanı zorunludur.")
                .MaximumLength(30).WithMessage("Ad ve soyad en fazla 30 karakter olabilir.");
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı alanı zorunludur.")
                .MaximumLength(30).WithMessage("Kullanıcı adı en fazla 30 karakter olabilir.");
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("E-posta alanı zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Şifre alanı zorunludur.")
                .MaximumLength(15).WithMessage("Şifre en fazla 15 karakter olabilir.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
            RuleFor(r => r.RePassword)
                .Equal(r => r.Password).WithMessage("Şifreler eşleşmelidir.");
            RuleFor(s => s).Custom((s, context) =>
            {
                if (s.Password != s.RePassword)
                {
                    context.AddFailure("Password", "paswords dont match in this part");
                }

            });

        }
    }
}
