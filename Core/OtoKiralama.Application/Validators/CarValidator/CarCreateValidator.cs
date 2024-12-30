using FluentValidation;
using OtoKiralama.Application.Dtos.Car;

namespace OtoKiralama.Application.Validators.CarValidator
{
    public class CarCreateValidator : AbstractValidator<CarCreateDto>
    {
        public CarCreateValidator()
        {
            // Plaka doğrulaması: Boş olamaz, uzunluğu 6 ile 10 karakter arasında olmalı
            RuleFor(x => x.Plate)
                .NotEmpty().WithMessage("Plaka alanı zorunludur.")
                .Length(6, 10).WithMessage("Plaka alanının uzunluğu en az 6, en fazla 10 karakter olmalıdır.");

            // Model doğrulaması: Boş olamaz
            RuleFor(x => x.ModelId)
                .GreaterThan(0).WithMessage("Model alanı boş olamaz.");

            // Koltuk Sayısı doğrulaması: 1 ile 9 arasında olmalı
            RuleFor(x => x.SeatCount)
                .InclusiveBetween(1, 9).WithMessage("Koltuk sayısı 1 ile 9 arasında olmalıdır.");

            // Günlük Fiyat doğrulaması: 0'dan büyük olmalı
            RuleFor(x => x.DailyPrice)
                .GreaterThan(0).WithMessage("Günlük fiyat 0'dan büyük olmalıdır.");

            // Üretim Yılı doğrulaması: Geçerli bir yıl olmalı
            RuleFor(x => x.Year)
                .InclusiveBetween(1886, DateTime.Now.Year).WithMessage($"Üretim yılı 1886 ile {DateTime.Now.Year} arasında olmalıdır.");

            // Marka doğrulaması: Boş olamaz
            RuleFor(x => x.BrandId)
                .GreaterThan(0).WithMessage("Marka alanı boş olamaz.");

            // Şirket doğrulaması: Boş olamaz
            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("Şirket alanı boş olamaz.");

            // Sınıf doğrulaması: Boş olamaz
            RuleFor(x => x.ClassId)
                .GreaterThan(0).WithMessage("Sınıf alanı boş olamaz.");

            // Yakıt doğrulaması: Boş olamaz
            RuleFor(x => x.FuelId)
                .GreaterThan(0).WithMessage("Yakıt alanı boş olamaz.");

            // Vites doğrulaması: Boş olamaz
            RuleFor(x => x.GearId)
                .GreaterThan(0).WithMessage("Vites alanı boş olamaz.");

            // Gövde doğrulaması: Boş olamaz
            RuleFor(x => x.BodyId)
                .GreaterThan(0).WithMessage("Gövde alanı boş olamaz.");

            // Konum doğrulaması: Boş olamaz
            RuleFor(x => x.LocationId)
                .GreaterThan(0).WithMessage("Konum alanı boş olamaz.");
        }
    }
}