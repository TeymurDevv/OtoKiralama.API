using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Application.Dtos.Pagination;

namespace OtoKiralama.Application.Interfaces
{
    public interface IBrandService
    {
        Task<PagedResponse<BrandListItemDto>> GetAllBrandsAsync(int pageNumber, int pageSize);
        Task<BrandReturnDto> GetBrandByIdAsync(int id);
        Task CreateBrandAsync(BrandCreateDto brandCreateDto);
        Task DeleteBrandAsync(int id);
        Task UpdateBrandAsync(int id, BrandUpdateDto brandUpdateDto);
    }
}
