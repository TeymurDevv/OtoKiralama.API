
using OtoKiralama.Application.Dtos.Fuel;

namespace OtoKiralama.Application.Interfaces
{
    public interface IFuelService
    {
        Task<List<FuelReturnDto>> GetAllFuelsAsync();
        Task<FuelReturnDto> GetFuelByIdAsync(int id);
        Task CreateFuelAsync(FuelCreateDto fuelCreateDto);
        Task DeleteFuelAsync(int id);
    }
}
