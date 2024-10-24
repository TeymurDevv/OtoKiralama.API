using OtoKiralama.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtoKiralama.Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string AppUserId { get; set; }
        public IAppUser AppUser { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPrice { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }
    }
}
