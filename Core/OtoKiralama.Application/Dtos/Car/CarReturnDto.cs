namespace OtoKiralama.Application.Dtos.Car
{
    public class CarReturnDto
    {
        public string Plate { get; set; }
        public string Model { get; set; }
        public int SeatCount { get; set; }
        public double DailyPrice { get; set; }
        public int Year { get; set; }
        public bool IsInstantConfirm { get; set; }
        public bool IsFreeRefund { get; set; }
        public string BodyName { get; set; }
        public string BrandName { get; set; }
        public string ClassName { get; set; }
        public string FuelName { get; set; }
        public string GearName { get; set; }
        public string LocationName { get; set; }
    }
}
