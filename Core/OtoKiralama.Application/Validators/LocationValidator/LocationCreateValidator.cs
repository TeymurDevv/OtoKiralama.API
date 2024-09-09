using FluentValidation;
using OtoKiralama.Application.Dtos.Location;

namespace OtoKiralama.Application.Validators.LocationValidator
{
    public class LocationCreateValidator : AbstractValidator<LocationCreateDto>
    {
        public LocationCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
