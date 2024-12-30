using OtoKiralama.Domain.Enums;

namespace OtoKiralama.Domain.Entities
{
    public interface IAppUser
    {
        string FullName { get; set; }
        int? CompanyId { get; set; }
        Company Company { get; set; }
        List<Reservation> Reservations { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public UserIdentityInformation? UserIdentityInformation { get; set; }
    }
}
