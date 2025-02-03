using OtoKiralama.Application.Dtos.Body;
using OtoKiralama.Application.Dtos.Class;
using OtoKiralama.Application.Dtos.Company;
using OtoKiralama.Application.Dtos.DeliveryType;
using OtoKiralama.Application.Dtos.Fuel;
using OtoKiralama.Application.Dtos.Gear;
using OtoKiralama.Application.Dtos.Location;
using OtoKiralama.Application.Dtos.Model;

namespace OtoKiralama.Application.Dtos.Car
{
    public class CarListItemDto
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public int SeatCount { get; set; }
        public double DailyPrice { get; set; }
        public int Year { get; set; }
        public bool IsInstantConfirm { get; set; } // Rezervasyon onayı gerekli mi?
        public bool IsFreeRefund { get; set; } // İptal durumunda ücretsiz geri ödeme var mı?
        public bool IsActive { get; set; } // Araç aktif mi, kullanıma hazır mı?
        public bool IsReserved { get; set; } // Şu anda rezervasyonlu mu?
        public BodyReturnDto Body { get; set; }
        public ModelReturnDto Model { get; set; }
        public ClassReturnDto Class { get; set; }
        public FuelReturnDto Fuel { get; set; }
        public GearReturnDto Gear { get; set; }
        public LocationReturnDto Location { get; set; }
        public CompanyReturnDto Company { get; set; }

        //yeni yaradilan entitiler
        public bool IsLimited { get; private set; } = false;
        public int? Limit { get; private set; }
        public DeliveryTypeReturnDto DeliveryType { get; set; }
        public int DepositAmount { get; set; }
    }
}
