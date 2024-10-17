using OtoKiralama.Application.Dtos.Car;
using OtoKiralama.Application.Dtos.CarPhoto;
using OtoKiralama.Application.Dtos.Pagination;

namespace OtoKiralama.Application.Interfaces
{
    public interface ICarPhotoService
    {
        Task<PagedResponse<CarPhotoListItemDto>> GetAllCarPhotosAsync(int pageNumber, int pageSize);
        Task<CarPhotoReturnDto> GetCarPhotoByIdAsync(int id);
        Task CreateCarPhotoAsync(CarPhotoCreateDto carPhotoCreateDto);
        Task DeleteCarPhotoAsync(int id);
    }
}
