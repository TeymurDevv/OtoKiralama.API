using OtoKiralama.Domain.Common;

namespace OtoKiralama.Domain.Entities
{
    public class Car : BaseEntity
    {
        public int ModelId { get; set; }
        public int CompanyId { get; set; }
        public int ClassId { get; set; }
        public int LocationId { get; set; }
        public Model Model { get; set; }
        public Company Company { get; set; }
        public Location Location { get; set; }
        public Class Class { get; set; }
        public CarDetail CarDetail { get; set; }
        public double DailyPrice { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFreeRefund { get; set; }
        public bool IsInstantConfirm { get; set; }
        public int DistanceLimit { get; set; }
    }
}
