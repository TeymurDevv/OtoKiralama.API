using OtoKiralama.Application.Dtos.Company;

namespace OtoKiralama.Application.Dtos.User
{
    public class UserReturnDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TcKimlik { get; set; }
        public DateTime? BirthDate { get; set; }
        public CompanyReturnDto Company { get; set; }
    }
}
