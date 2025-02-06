using OtoKiralama.Application.Dtos.Car.CarSearchDtos;

namespace OtoKiralama.Application.Dtos.Car
{
    public class CarSearchListDto
    {
        public List<int> GearIds { get; set; }
        public List<int> CompanyIds { get; set; }
        public List<int> FuelIds { get; set; }
        public List<int> BrandIds { get; set; }
        public List<int> ModelIds { get; set; }
        public List<int> SeatCounts { get; set; }
        public List<int> DeliveryTypeIds { get; set; }
        public DepositAmountDto DepositAmountRange { get; set; }
        public LimitDto LimitRange { get; set; }
        public DailyPriceDto DailyPriceRange { get; set; }

    }
}
