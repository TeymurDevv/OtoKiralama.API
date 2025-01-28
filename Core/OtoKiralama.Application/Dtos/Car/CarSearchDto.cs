namespace OtoKiralama.Application.Dtos.Car;

public class CarSearchDto
{
    public int PickupLocationId { get; set; }
    public int? DropOffLocationId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}