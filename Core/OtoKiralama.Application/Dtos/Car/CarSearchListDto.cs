namespace OtoKiralama.Application.Dtos.Car
{
    public class CarSearchListDto
    {
        public int PickupLocationId { get; set; }
        public int? DropoffLocationId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public List<int> GearIds { get; set; }
        public List<int> CompanyIds { get; set; }
        public List<int> FuelIds { get; set; }
        public List<int> BrandIds { get; set; }
        public List<int> ModelIds { get; set; }
        public List<int> SeatCounts { get; set; }
        public List<int> DeliveryTypeIds { get; set; }
        public List<int> ClassIds { get; set; }

        public int? DepositAmountRangeId { get; set; }
        public int? LimitRangeId { get; set; }
        public int? DailyPriceRangeId { get; set; }
    }

}
