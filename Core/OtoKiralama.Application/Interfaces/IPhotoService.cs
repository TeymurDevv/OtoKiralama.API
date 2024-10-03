using Microsoft.AspNetCore.Http;

namespace OtoKiralama.Application.Interfaces
{
    public interface IPhotoService
    {
        Task<string> UploadPhotoAsync(IFormFile file);
        Task DeletePhotoAsync(string imageUrl);
    }
}
