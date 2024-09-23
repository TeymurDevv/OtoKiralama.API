namespace OtoKiralama.Application.Interfaces
{
    public interface IGearService
    {
        Task<List<GearReturnDto>> GetAllGearsAsync();
        Task<GearReturnDto> GetGearByIdAsync(int id);
        Task CreateGearAsync(GearCreateDto gearCreateDto);
        Task DeleteGearAsync(int id);
    }
}
