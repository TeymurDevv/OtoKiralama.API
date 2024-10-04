using OtoKiralama.Application.Dtos.Body;
using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Application.Dtos.Location;
using OtoKiralama.Application.Dtos.Pagination;

namespace OtoKiralama.Application.Interfaces
{
    public interface ILocationService
    {
        Task<PagedResponse<LocationListItemDto>> GetAllLocationsAsync(int pageNumber, int pageSize);
        Task<LocationReturnDto> GetLocationByIdAsync(int id);
        Task CreateLocationAsync(LocationCreateDto locationCreateDto);
        Task DeleteLocationAsync(int id);
    }
}
