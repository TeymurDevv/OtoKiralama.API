using OtoKiralama.Application.Dtos.DeliveryType;
using OtoKiralama.Application.Dtos.Pagination;

namespace OtoKiralama.Application.Interfaces
{
    public interface IDeliveryTypeService
    {
        Task<PagedResponse<DeliveryTypeListItemDto>> GetAllDeliveryTypesAsync(int pageNumber, int pageSize);
        Task<DeliveryTypeReturnDto> GetDeliveryTypeByIdAsync(int id);
        Task CreateDeliveryTypeAsync(DeliveryTypeCreateDto deliveryTypeCreateDto);
        Task DeleteDeliveryTypeAsync(int id);
    }
}
