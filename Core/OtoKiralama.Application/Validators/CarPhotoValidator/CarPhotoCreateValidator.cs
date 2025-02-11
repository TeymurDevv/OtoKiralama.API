using FluentValidation;
using OtoKiralama.Application.Dtos.CarPhoto;

namespace OtoKiralama.Application.Validators.CarPhotoValidator
{
    public class CarPhotoCreateValidator : AbstractValidator<CarPhotoCreateDto>
    {
        public CarPhotoCreateValidator()
        {
            RuleFor(x => x.ModelId)
                .GreaterThan(0)
                .WithMessage("ModelId must be greater than 0");

            RuleFor(x => x.Image)
                .NotEmpty()
                .WithMessage("Image is required for CarPhoto");
        }
    }
}
