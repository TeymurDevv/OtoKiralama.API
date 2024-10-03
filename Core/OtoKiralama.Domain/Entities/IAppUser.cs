namespace OtoKiralama.Domain.Entities
{
    public interface IAppUser
    {
        string FullName { get; set; }
        int? CompanyId { get; set; }
        Company Company { get; set; }
    }
}
