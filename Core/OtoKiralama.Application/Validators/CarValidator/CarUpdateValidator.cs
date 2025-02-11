using FluentValidation;
using OtoKiralama.Application.Dtos.Car;

namespace OtoKiralama.Application.Validators.CarValidator
{
    public class CarUpdateValidator : AbstractValidator<CarUpdateDto>
    {
        public CarUpdateValidator()
        {
            RuleFor(x => x.Plate)
                .NotEmpty().WithMessage("Plaka alanı zorunludur.")
                .Length(6, 10).WithMessage("Plaka alanının uzunluğu en az 6, en fazla 10 karakter olmalıdır.");

            RuleFor(x => x.SeatCount)
                .InclusiveBetween(1, 9).WithMessage("Koltuk sayısı 1 ile 9 arasında olmalıdır.");

            RuleFor(x => x.DailyPrice)
                .GreaterThan(0).WithMessage("Günlük fiyat 0'dan büyük olmalıdır.");

            RuleFor(x => x.Year)
                .InclusiveBetween(1886, DateTime.Now.Year).WithMessage($"Üretim yılı 1886 ile {DateTime.Now.Year} arasında olmalıdır.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("Şirket alanı boş olamaz.");

            RuleFor(x => x.BodyId)
                .GreaterThan(0).WithMessage("Gövde alanı boş olamaz.");

            RuleFor(x => x.ModelId)
                .GreaterThan(0).WithMessage("Model alanı boş olamaz.");

            RuleFor(x => x.ClassId)
                .GreaterThan(0).WithMessage("Sınıf alanı boş olamaz.");

            RuleFor(x => x.FuelId)
                .GreaterThan(0).WithMessage("Yakıt alanı boş olamaz.");

            RuleFor(x => x.GearId)
                .GreaterThan(0).WithMessage("Vites alanı boş olamaz.");

            RuleFor(x => x.LocationId)
                .GreaterThan(0).WithMessage("Konum alanı boş olamaz.");

            RuleFor(x => x.DeliveryTypeId)
                .GreaterThan(0).WithMessage("Teslimat tipi boş olamaz.");

            RuleFor(x => x.DepositAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Deposit amount sıfırdan az ola bilməz.");
        }
    }
}
