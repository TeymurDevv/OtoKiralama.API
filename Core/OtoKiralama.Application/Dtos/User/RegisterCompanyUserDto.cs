namespace OtoKiralama.Application.Dtos.User
{
    public class RegisterCompanyUserDto
    {
        public int CompanyId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
