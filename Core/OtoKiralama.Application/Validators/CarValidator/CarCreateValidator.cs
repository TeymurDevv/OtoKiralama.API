using FluentValidation;
using OtoKiralama.Application.Dtos.Car;

namespace OtoKiralama.Application.Validators.CarValidator
{
    public class CarCreateValidator : AbstractValidator<CarCreateDto>
    {
        public CarCreateValidator()
        {
            // Plate validation: Must not be empty, length between 6 and 10
            RuleFor(x => x.Plate)
                .NotEmpty().WithMessage("Plate is required.")
                .Length(6, 10).WithMessage("Plate must be between 6 and 10 characters.");

            // CompanyId validation: Must be greater than 0
            RuleFor(x => x.ModelId)
                .GreaterThan(0).WithMessage("ModelId must be greater than 0.");

            // SeatCount validation: Must be between 1 and 9
            RuleFor(x => x.SeatCount)
                .InclusiveBetween(1, 9).WithMessage("Seat count must be between 1 and 9.");

            // DailyPrice validation: Must be greater than 0
            RuleFor(x => x.DailyPrice)
                .GreaterThan(0).WithMessage("Daily price must be greater than 0.");

            // Year validation: Must be a valid year
            RuleFor(x => x.Year)
                .InclusiveBetween(1886, DateTime.Now.Year).WithMessage($"Year must be between 1886 and {DateTime.Now.Year}.");


            // BrandId validation: Must be greater than 0
            RuleFor(x => x.BrandId)
                .GreaterThan(0).WithMessage("BrandId must be greater than 0.");

            // CompanyId validation: Must be greater than 0
            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            // ClassId validation: Must be greater than 0
            RuleFor(x => x.ClassId)
                .GreaterThan(0).WithMessage("ClassId must be greater than 0.");

            // FuelId validation: Must be greater than 0
            RuleFor(x => x.FuelId)
                .GreaterThan(0).WithMessage("FuelId must be greater than 0.");

            // GearId validation: Must be greater than 0
            RuleFor(x => x.GearId)
                .GreaterThan(0).WithMessage("GearId must be greater than 0.");

            // BodyId validation: Must be greater than 0
            RuleFor(x => x.BodyId)
                .GreaterThan(0).WithMessage("BodyId must be greater than 0.");

            // LocationId validation: Must be greater than 0
            RuleFor(x => x.LocationId)
                .GreaterThan(0).WithMessage("LocationId must be greater than 0.");
        }
    }
}
