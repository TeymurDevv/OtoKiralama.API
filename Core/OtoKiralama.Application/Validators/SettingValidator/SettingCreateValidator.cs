using FluentValidation;
using OtoKiralama.Application.Dtos.Setting;

namespace OtoKiralama.Application.Validators.SettingValidator
{
    public class SettingCreateValidator:AbstractValidator<SettingCreateDto>
    {
        public SettingCreateValidator()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(s => s.Key).NotEmpty().WithMessage("Key is required");
            RuleFor(s => s.Value).NotEmpty().WithMessage("Value is required");
        }
    }
}
