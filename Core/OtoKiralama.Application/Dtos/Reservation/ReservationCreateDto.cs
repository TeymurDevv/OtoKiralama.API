namespace OtoKiralama.Application.Dtos.Reservation
{
    public class ReservationCreateDto
    {
        public int CarId { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }
    }
}
