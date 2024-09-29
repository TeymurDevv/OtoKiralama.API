using OtoKiralama.Application.Dtos.Gear;

namespace OtoKiralama.Application.Interfaces
{
    public interface IFuelService
    {
        Task<List<GearReturnDto>> GetAllFuelsAsync();
        Task<GearReturnDto> GetFuelByIdAsync(int id);
        Task CreateFuelAsync(GearCreateDto gearCreateDto);
        Task DeleteFuelAsync(int id);
    }
}
