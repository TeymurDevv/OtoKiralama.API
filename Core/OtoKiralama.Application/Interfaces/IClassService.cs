using OtoKiralama.Application.Dtos.Class;

namespace OtoKiralama.Application.Interfaces
{
    public interface IClassService
    {
        Task<List<ClassReturnDto>> GetAllClassesAsync();
        Task<ClassReturnDto> GetClassByIdAsync(int id);
        Task CreateClassAsync(ClassCreateDto classCreateDto);
        Task DeleteClassAsync(int id);
    }
}
