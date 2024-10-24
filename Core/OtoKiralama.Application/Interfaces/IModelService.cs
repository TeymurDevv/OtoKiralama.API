using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Application.Dtos.Model;
using OtoKiralama.Application.Dtos.Pagination;

namespace OtoKiralama.Application.Interfaces
{
    public interface IModelService
    {
        Task<PagedResponse<ModelListItemDto>> GetAllModelsAsync(int pageNumber, int pageSize);
        Task<PagedResponse<ModelListItemDto>> GetAllModelsByBrandId(int brandId, int pageNumber, int pageSize);
        Task<ModelReturnDto> GetModelByIdAsync(int id);
        Task CreateModelAsync(ModelCreateDto modelCreateDto);
        Task DeleteModelAsync(int id);
    }
}
