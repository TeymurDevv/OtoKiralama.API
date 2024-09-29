using OtoKiralama.Application.Dtos.Body;
using OtoKiralama.Application.Dtos.Gear;

namespace OtoKiralama.Application.Interfaces
{
    public interface IBodyService
    {
        Task<List<BodyReturnDto>> GetAllBodiesAsync();
        Task<BodyReturnDto> GetBodyByIdAsync(int id);
        Task CreateBodyAsync(BodyCreateDto bodyCreateDto);
        Task DeleteBodyAsync(int id);
    }
}
