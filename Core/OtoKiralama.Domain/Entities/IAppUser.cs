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
        bool IsEmailSubscribed { get; set; }
        public string? TcKimlik { get; set; }
        bool IsSmsSubscribed { get; set; }
    }
}
