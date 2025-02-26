namespace OtoKiralama.Application.Dtos.Car.CarSearchDtos
{
    public class CarSearchResultDto
    {
        public List<CarListItemDto> Cars { get; set; }
        public CarFilterCountsDto FilterCounts { get; set; }
    }
}
