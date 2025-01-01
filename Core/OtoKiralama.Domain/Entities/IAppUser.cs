using OtoKiralama.Domain.Enums;

namespace OtoKiralama.Domain.Entities
{
    public interface IAppUser
    {
        string FullName { get; set; }
        int? CompanyId { get; set; }
        Company Company { get; set; }
        List<Reservation> Reservations { get; set; }
         DateTime CreatedDate { get; set; }
         DateTime? BirthDate { get; set; }
         UserIdentityInformation? UserIdentityInformation { get; set; }
         bool IsEmailSubscribed { get; set; }
         bool IsSmsSubscribed { get; set; }
    }
}
