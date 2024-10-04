using OtoKiralama.Application.Dtos.Body;
using OtoKiralama.Application.Dtos.Gear;
using OtoKiralama.Application.Dtos.Pagination;

namespace OtoKiralama.Application.Interfaces
{
    public interface IGearService
    {
        Task<PagedResponse<GearListItemDto>> GetAllGearsAsync(int pageNumber, int pageSize);
        Task<GearReturnDto> GetGearByIdAsync(int id);
        Task CreateGearAsync(GearCreateDto gearCreateDto);
        Task DeleteGearAsync(int id);
    }
}
