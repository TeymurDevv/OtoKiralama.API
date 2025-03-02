using OtoKiralama.Application.Dtos.FilterRange;
using OtoKiralama.Application.Dtos.Pagination;

namespace OtoKiralama.Application.Interfaces
{
    public interface IFilterRangeService
    {
        Task<PagedResponse<FilterRangeReturnDto>> GetAllFilterRangeAsync(int pageNumber, int pageSize);
        Task<FilterRangeReturnDto> GetFilterRangeByIdAsync(int id);
        Task CreateFilterRangeAsync(FilterRangeCreateDto filterRangeCreateDto);
        Task DeleteFilterRangeAsync(int id);
        Task UpdateFilterRangeAsync(int id, FilterRangeUpdateDto filterRangeUpdateDto);
    }
}
