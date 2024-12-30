using FluentValidation;
using OtoKiralama.Application.Dtos.Setting;

namespace OtoKiralama.Application.Validators.SettingValidator
{
    public class SettingCreateValidator:AbstractValidator<SettingCreateDto>
    {
        public SettingCreateValidator()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("Ad alanı zorunludur.");
            RuleFor(s => s.Key).NotEmpty().WithMessage("Anahtar alanı zorunludur.");
            RuleFor(s => s.Value).NotEmpty().WithMessage("Değer alanı zorunludur.");
        }
    }
}
