using OtoKiralama.Application.Dtos.Body;
using OtoKiralama.Application.Dtos.Gear;

namespace OtoKiralama.Application.Interfaces
{
    public interface IBodyService
    {
        Task<List<BodyReturnDto>> GetAllGearsAsync();
        Task<BodyReturnDto> GetGearByIdAsync(int id);
        Task CreateGearAsync(BodyCreateDto bodyCreateDto);
        Task DeleteGearAsync(int id);
    }
}
