using FluentValidation;
using OtoKiralama.Application.Dtos.Company;

namespace OtoKiralama.Application.Validators.CompanyValidator
{
    public class CompanyUpdateValidator : AbstractValidator<CompanyUpdateDto>
    {
        public CompanyUpdateValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası zorunludur.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Geçersiz telefon numarası formatı.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres alanı zorunludur.")
                .MaximumLength(200).WithMessage("Adres 200 karakterden uzun olamaz.");

        }
    }
}

