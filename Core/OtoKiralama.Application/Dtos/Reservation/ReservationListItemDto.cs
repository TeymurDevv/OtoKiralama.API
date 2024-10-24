using OtoKiralama.Application.Dtos.Car;
using OtoKiralama.Application.Dtos.User;

namespace OtoKiralama.Application.Dtos.Reservation
{
    public class ReservationListItemDto
    {
        public CarReturnDto Car { get; set; }
        public UserReturnDto User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPrice { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }
    }
}
