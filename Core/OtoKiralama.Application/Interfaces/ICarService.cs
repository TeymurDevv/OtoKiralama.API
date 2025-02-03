using OtoKiralama.Application.Dtos.Car;
using OtoKiralama.Application.Dtos.Pagination;

namespace OtoKiralama.Application.Interfaces
{
    public interface ICarService
    {
        Task<PagedResponse<CarListItemDto>> GetAllCarsAsync(int pageNumber, int pageSize);
        Task<CarReturnDto> GetCarByIdAsync(int id);
        Task CreateCarAsync(CarCreateDto carCreateDto);
        Task DeleteCarAsync(int id);
        Task ChangeCarStatus(int id);
        Task MarkAsDeactıve(int id);

        Task<List<CarListItemDto>> GetAllFilteredCarsAsync(int pickupLocationId, int? dropoffLocationId, DateTime startDate,
            DateTime endDate);
        Task<List<CarListItemDto>> GetAllFilteredListCarsAsync(CarSearchListDto carSearchListDto);


    }
}
