using FluentValidation;
using OtoKiralama.Application.Dtos.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoKiralama.Application.Validators.LocationValidator
{
    public class LocationUpdateDtoValidator : AbstractValidator<LocationUpdateDto>
    {
        public LocationUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı zorunludur.");
        }
    }
}
