namespace OtoKiralama.Application.Dtos.Car
{
    public class CarSearchListDto
    {
        public List<int> GearIds { get; set; }
        public List<int> CompanyIds { get; set; }
        public List<int> FuelIds { get; set; }
        public List<int> BrandIds { get; set; }
        public List<int> ModelIds { get; set; }
        public List<int> Limits { get; private set; }
        public List<double> DailyPrices { get; set; }
        public List<int> SeatCounts { get; set; }
        public List<int> DeliveryTypeIds { get; set; }
        public List<int> DepositAmounts { get; set; }

    }
}
