using OtoKiralama.Application.Dtos.Brand;

namespace OtoKiralama.Application.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandReturnDto>> GetAllBrandsAsync();
        Task<BrandReturnDto> GetBrandByIdAsync(int id);
        Task CreateBrandAsync(BrandCreateDto brandCreateDto);
        Task DeleteBrandAsync(int id);
    }
}
