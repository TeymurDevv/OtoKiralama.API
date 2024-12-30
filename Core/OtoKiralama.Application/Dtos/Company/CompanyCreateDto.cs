using Microsoft.AspNetCore.Http;

namespace OtoKiralama.Application.Dtos.Company
{
    public class CompanyCreateDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Requirements { get; set; }
        public string PaymentInformation { get; set; }
        public string Trust { get; set; }
        public string Essentials { get; set; }
    }
}
