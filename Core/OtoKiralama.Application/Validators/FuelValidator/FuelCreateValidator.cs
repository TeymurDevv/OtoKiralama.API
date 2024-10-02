using FluentValidation;
using OtoKiralama.Application.Dtos.Fuel;

namespace OtoKiralama.Application.Validators.FuelValidator
{
    public class FuelCreateValidator : AbstractValidator<FuelCreateDto>
    {
        public FuelCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x=> x.Name).MaximumLength(50).WithMessage("Name can't be more than 50 character");
            RuleFor(x=>x.Name).MinimumLength(3).WithMessage("Name can't be more than 3 character");
        }
    }
}
