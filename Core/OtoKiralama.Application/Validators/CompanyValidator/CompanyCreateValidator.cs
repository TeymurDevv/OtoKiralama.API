using FluentValidation;
using Microsoft.AspNetCore.Http;
using OtoKiralama.Application.Dtos.Company;
namespace OtoKiralama.Application.Validators.CompanyValidator
{
    public class CompanyCreateValidator:AbstractValidator<CompanyCreateDto>
    {
        public CompanyCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage("An image file is required.")
                .Must(BeAValidImage).WithMessage("Only image files (jpg, jpeg, png) are allowed.");
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
