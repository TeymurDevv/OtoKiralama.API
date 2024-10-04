using Microsoft.AspNetCore.Http;

namespace OtoKiralama.Application.Dtos.Company
{
    public class CompanyCreateDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
