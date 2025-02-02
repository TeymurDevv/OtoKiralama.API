using Microsoft.AspNetCore.Http;

namespace OtoKiralama.Application.Dtos.CarPhoto
{
    public class CarPhotoCreateDto
    {
        public int ModelId { get; set; }
        public IFormFile Image { get; set; }
    }
}
