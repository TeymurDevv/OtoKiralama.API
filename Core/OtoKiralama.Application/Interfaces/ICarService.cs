using OtoKiralama.Application.Dtos.Car;
using OtoKiralama.Application.Dtos.Fuel;
using OtoKiralama.Application.Dtos.Pagination;

namespace OtoKiralama.Application.Interfaces
{
    public interface ICarService
    {
        Task<PagedResponse<CarListItemDto>> GetAllCarsAsync(int pageNumber, int pageSize);
        Task<CarReturnDto> GetCarByIdAsync(int id);
        Task CreateCarAsync(CarCreateDto carCreateDto);
        Task DeleteCarAsync(int id);
    }
}
