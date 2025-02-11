using FluentValidation;
using Microsoft.AspNetCore.Http;
using OtoKiralama.Application.Dtos.Company;

namespace OtoKiralama.Application.Validators.CompanyValidator
{
    public class CompanyFullUpdateValidator : AbstractValidator<CompanyFullUpdateDto>
    {
        public CompanyFullUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Şirket adı zorunludur.")
                .MaximumLength(100).WithMessage("Şirket adı 100 karakterden uzun olamaz.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası zorunludur.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Geçersiz telefon numarası formatı.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres alanı zorunludur.")
                .MaximumLength(200).WithMessage("Adres 200 karakterden uzun olamaz.");

            RuleFor(x => x.ImageFile)
              .Must(BeAValidImage).WithMessage("Sadece resim dosyalarına (jpg, jpeg, png) izin verilir.")
              .When(x => x.ImageFile != null);

        }

        private bool BeAValidImage(IFormFile file)
        {
            if (file == null)
                return false;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(extension);
        }
    }
}
