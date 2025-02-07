using OtoKiralama.Domain.Entities.Common;
using OtoKiralama.Domain.Enums;

namespace OtoKiralama.Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public string ReservationNumber { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string AppUserId { get; set; }
        public IAppUser AppUser { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPrice { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsCompleted { get; set; }
        private ReservationStatus _status;

        public ReservationStatus Status
        {
            get
            {
                if (IsCanceled) return ReservationStatus.Canceled;
                if (IsCompleted) return ReservationStatus.Completed;
                else if (DateTime.Now < StartDate) return ReservationStatus.Pending;
                else if (DateTime.Now >= StartDate && DateTime.Now <= EndDate) return ReservationStatus.InProgress;
                return _status; // Return explicitly set status
            }
            set
            {
                _status = value;
            }
        }
    }
}
