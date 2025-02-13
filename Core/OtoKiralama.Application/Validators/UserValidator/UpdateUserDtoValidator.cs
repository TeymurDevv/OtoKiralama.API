using FluentValidation;
using OtoKiralama.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoKiralama.Application.Validators.UserValidator
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {private int CalculateAge(DateTime? birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate?.Year;

        if (birthDate > today.AddYears((int)-age)) age--;

        return (int)age;
    }
        public UpdateUserDtoValidator()
        {
            RuleFor(s => s.FullName).MaximumLength(100).WithMessage("max is 100").When(s => !string.IsNullOrWhiteSpace(s.Email));
            RuleFor(s=>s.PhoneNumber).Matches(@"^(?:\+90|0)?\s?(5\d{2})\s?(\d{3})\s?(\d{2})\s?(\d{2})$")
            .WithMessage("Invalid phone number format.").When(s => !string.IsNullOrWhiteSpace(s.PhoneNumber));
            RuleFor(s => s.Email).EmailAddress().When(s => !string.IsNullOrWhiteSpace(s.Email));
            RuleFor(s => s.BirthDate).Must(birthDate => CalculateAge(birthDate) >= 18)
            .WithMessage("The person must be at least 18 years old.").When(s => s.BirthDate is not null);
            RuleFor(p => p.BirthDate)
                .NotEmpty()
                .NotEmpty()
                .WithMessage("Birth date is required.");
            RuleFor(p => p.FullName
                )
                .NotEmpty()
                .NotEmpty()
                .WithMessage("FullName is required.");
            RuleFor(p => p.PhoneNumber)
                .NotEmpty()
                .NotEmpty()
                .WithMessage("PhoneNumber is required.");
            RuleFor(p => p.TcKimlik)
                .NotEmpty()
                .NotEmpty()
                .WithMessage("TcKimlik is required.");
            RuleFor(p => p.Email)
                .NotEmpty()
                .NotEmpty()
                .WithMessage("Email is required.");

        }
    }
}
