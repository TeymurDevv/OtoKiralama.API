using OtoKiralama.Application.Dtos.Gear;

namespace OtoKiralama.Application.Interfaces
{
    public interface IFuelService
    {
        Task<List<GearReturnDto>> GetAllGearsAsync();
        Task<GearReturnDto> GetGearByIdAsync(int id);
        Task CreateGearAsync(GearCreateDto gearCreateDto);
        Task DeleteGearAsync(int id);
    }
}
