using OtoKiralama.Application.Dtos.Body;
using OtoKiralama.Application.Dtos.Pagination;

namespace OtoKiralama.Application.Interfaces
{
    public interface IBodyService
    {
        Task<PagedResponse<BodyListItemDto>> GetAllBodiesAsync(int pageNumber, int pageSize);
        Task<BodyReturnDto> GetBodyByIdAsync(int id);
        Task CreateBodyAsync(BodyCreateDto bodyCreateDto);
        Task DeleteBodyAsync(int id);
        Task UpdateBodyAsync(int id, BodyUpdateDto bodyUpdateDto);
    }
}
