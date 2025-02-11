using FluentValidation;
using OtoKiralama.Application.Dtos.Fuel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoKiralama.Application.Validators.FuelValidator
{
    public class FuelUpdateDtoValidator : AbstractValidator<FuelUpdateDto>
    {
        public FuelUpdateDtoValidator()
        {
            RuleFor(c => c.Name)
           .NotEmpty()
           .WithMessage("İsim alanı boş olamaz.")
           .MinimumLength(2)
           .WithMessage("isim metni en az 2 karakter uzunluğunda olmak zorunda.")
            .MaximumLength(50)
           .WithMessage("isim metni en fazla 50 karakter uzunluğunda ola bilir.");
        }
    }
    }
}
