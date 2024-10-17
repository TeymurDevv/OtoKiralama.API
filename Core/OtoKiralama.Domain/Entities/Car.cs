using OtoKiralama.Domain.Entities.Common;

namespace OtoKiralama.Domain.Entities
{
    public class Car:BaseEntity
    {
        public string Plate { get; set; }
        public int SeatCount { get; set; }
        public double DailyPrice { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public bool IsInstantConfirm { get; set; }
        public bool IsFreeRefund { get; set; }
        public int BodyId { get; set; }
        public Body Body { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int FuelId { get; set; }
        public Fuel Fuel { get; set; }
        public int GearId { get; set; }
        public Gear Gear { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
