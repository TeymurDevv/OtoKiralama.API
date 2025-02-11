using FluentValidation;
using OtoKiralama.Application.Dtos.DeliveryType;

namespace OtoKiralama.Application.Validators.DeliveryTypeValidator
{
    public class DeliveryTypeUpdateDtoValidator : AbstractValidator<DeliveryTypeUpdateDto>
    {
        public DeliveryTypeUpdateDtoValidator()
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
