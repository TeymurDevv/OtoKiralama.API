namespace OtoKiralama.Application.Dtos.Car.CarSearchDtos
{
    public class FilterRangeCountDto
    {
        public int Id { get; set; }
        public string Name { get; set; }  // "1000-2000 AZN" kimi
        public int Count { get; set; }  // Bu intervalda neçə maşın var
    }

}
