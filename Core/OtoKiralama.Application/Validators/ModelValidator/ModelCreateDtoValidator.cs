using FluentValidation;
using OtoKiralama.Application.Dtos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoKiralama.Application.Validators.ModelValidator
{
    public class ModelCreateDtoValidator : AbstractValidator<ModelCreateDto>
    {
        public ModelCreateDtoValidator()
        {
            RuleFor(c => c.Name)
           .NotEmpty()
           .WithMessage("İsim alanı boş olamaz.")
           .MinimumLength(2)
           .WithMessage("isim metni en az 2 karakter uzunluğunda olmak zorunda.")
           .MaximumLength(50)
           .WithMessage("isim metni en fazla 50 karakter uzunluğunda ola bilir.");
            RuleFor(s => s.BrandId).NotNull();
        }
    }
}
