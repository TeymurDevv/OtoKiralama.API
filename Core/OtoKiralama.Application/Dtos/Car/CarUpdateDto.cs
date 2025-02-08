namespace OtoKiralama.Application.Dtos.Car
{
    public class CarUpdateDto
    {
        public string Plate { get; set; }
        public int SeatCount { get; set; }
        public double DailyPrice { get; set; }
        public int Year { get; set; }
        public bool IsInstantConfirm { get; set; }
        public bool IsFreeRefund { get; set; }
        public int CompanyId { get; set; }
        public int BodyId { get; set; }
        public int ModelId { get; set; }
        public int ClassId { get; set; }
        public int FuelId { get; set; }
        public int GearId { get; set; }
        public int LocationId { get; set; }
        public int? Limit { get; set; }
        public int DeliveryTypeId { get; set; }
        public int DepositAmount { get; set; }
    }

}
