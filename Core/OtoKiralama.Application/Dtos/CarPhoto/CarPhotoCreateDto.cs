using Microsoft.AspNetCore.Http;

namespace OtoKiralama.Application.Dtos.CarPhoto
{
    public class CarPhotoCreateDto
    {
        public string Name { get; set; }
        public int ModelId { get; set; }
        public IFormFile Image { get; set; }
    }
}
