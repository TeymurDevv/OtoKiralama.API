using OtoKiralama.Domain.Common;

namespace OtoKiralama.Domain.Entities
{
    public class CarDetail : BaseEntity
    {
        public int SeatCount { get; set; }
        public string GearType { get; set; }
        public string FuelType { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
