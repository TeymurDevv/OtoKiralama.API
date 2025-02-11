using OtoKiralama.Application.Dtos.Class;
using OtoKiralama.Application.Dtos.Pagination;

namespace OtoKiralama.Application.Interfaces
{
    public interface IClassService
    {
        Task<PagedResponse<ClassListItemDto>> GetAllClassesAsync(int pageNumber, int pageSize);
        Task<ClassReturnDto> GetClassByIdAsync(int id);
        Task CreateClassAsync(ClassCreateDto classCreateDto);
        Task DeleteClassAsync(int id);
        Task UpdateClassAsync(int id, ClassUpdateDto classUpdateDto);
        Task UpdateAsync(int? id, ClassUpdateDto classUpdateDto);
    }
}
