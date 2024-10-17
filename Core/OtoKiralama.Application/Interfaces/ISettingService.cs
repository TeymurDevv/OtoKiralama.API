using OtoKiralama.Application.Dtos.Car;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Dtos.Setting;

namespace OtoKiralama.Application.Interfaces
{
    public interface ISettingService
    {
        Task<PagedResponse<SettingListItemDto>> GetAllSettingsAsync(int pageNumber, int pageSize);
        Task<SettingReturnDto> GetSettingByIdAsync(string key);
        Task CreateSettingAsync(SettingCreateDto settingCreateDto);
    }
}
