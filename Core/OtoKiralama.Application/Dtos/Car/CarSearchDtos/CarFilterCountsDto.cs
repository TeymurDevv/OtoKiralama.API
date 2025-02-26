namespace OtoKiralama.Application.Dtos.Car.CarSearchDtos
{
    public class CarFilterCountsDto
    {
        public List<FilterCountDto> GearCounts { get; set; }
        public List<FilterCountDto> CompanyCounts { get; set; }
        public List<FilterCountDto> FuelCounts { get; set; }
        public List<FilterCountDto> BrandCounts { get; set; }
        public List<FilterCountDto> ModelCounts { get; set; }
        public List<FilterCountDto> DeliveryTypeCounts { get; set; }
        public List<FilterCountDto> ClassCounts { get; set; }

        // Range-lərə görə saylar
        public List<FilterRangeCountDto> DepositRangeCounts { get; set; }
        public List<FilterRangeCountDto> LimitRangeCounts { get; set; }
        public List<FilterRangeCountDto> DailyPriceRangeCounts { get; set; }
    }

}
