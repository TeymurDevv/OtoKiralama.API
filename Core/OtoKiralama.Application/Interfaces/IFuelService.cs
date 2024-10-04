
using OtoKiralama.Application.Dtos.Body;
using OtoKiralama.Application.Dtos.Fuel;
using OtoKiralama.Application.Dtos.Pagination;

namespace OtoKiralama.Application.Interfaces
{
    public interface IFuelService
    {
        Task<PagedResponse<FuelListItemDto>> GetAllFuelsAsync(int pageNumber, int pageSize);
        Task<FuelReturnDto> GetFuelByIdAsync(int id);
        Task CreateFuelAsync(FuelCreateDto fuelCreateDto);
        Task DeleteFuelAsync(int id);
    }
}
