using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Application.Dtos.Location;

namespace OtoKiralama.Application.Interfaces
{
    public interface ILocationService
    {
        Task<List<LocationReturnDto>> GetAllLocationsAsync();
        Task<LocationReturnDto> GetLocationByIdAsync(int id);
        Task CreateLocationAsync(LocationCreateDto locationCreateDto);
        Task DeleteLocationAsync(int id);
    }
}
