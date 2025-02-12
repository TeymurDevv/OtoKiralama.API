using Microsoft.AspNetCore.Identity;

namespace OtoKiralama.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public List<Reservation> Reservations { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? TcKimlik { get; set; }
        public bool IsEmailSubscribed { get; set; }
        public bool IsSmsSubscribed { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }
        public Invoice Invoice { get; set; }
    }
}
