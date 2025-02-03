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
    public class CarReturnDto
    {
        public string Id { get; set; }
        public string Plate { get; set; }
        public int SeatCount { get; set; }
        public double DailyPrice { get; set; }
        public int Year { get; set; }
        public bool IsInstantConfirm { get; set; }
        public bool IsFreeRefund { get; set; }
        public bool IsActive { get; set; }
        public bool IsReserved { get; set; }
        public ModelReturnDto Model { get; set; }
        public BodyReturnDto Body { get; set; }
        public ClassReturnDto Class { get; set; }
        public FuelReturnDto Fuel { get; set; }
        public GearReturnDto Gear { get; set; }
        public CompanyReturnDto Company { get; set; }
        public LocationReturnDto Location { get; set; }
        public DeliveryTypeReturnDto DeliveryType { get; set; }
        public bool IsLimited { get; private set; } = false;
        public int? Limit { get; private set; }
        public int DepositAmount { get; set; }

    }
}
