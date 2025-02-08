using Microsoft.AspNetCore.Http;

namespace OtoKiralama.Application.Dtos.CarPhoto
{
    public class CarPhotoUpdateDto
    {
        public IFormFile Image { get; set; }
    }
}
