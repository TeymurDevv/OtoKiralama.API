using OtoKiralama.Application.Dtos.Car;
using OtoKiralama.Application.Dtos.User;
using OtoKiralama.Domain.Enums; 

namespace OtoKiralama.Application.Dtos.Reservation
{
    public class ReservationReturnDto
    {
        public CarReturnDto Car { get; set; }
        public UserReturnDto User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReservationNumber { get; set; }
        public double TotalPrice { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }

        // Use ReservationStatus enum to reflect the status of the reservation
        public ReservationStatus Status
        {
            get
            {
                if (IsCanceled) return ReservationStatus.Canceled;
                else if (DateTime.Now < StartDate) return ReservationStatus.Pending;
                else if (DateTime.Now >= StartDate && DateTime.Now <= EndDate) return ReservationStatus.InProgress;
                else if (DateTime.Now > EndDate) return ReservationStatus.Completed;
                return ReservationStatus.Pending;
            }
        }
    }
}
