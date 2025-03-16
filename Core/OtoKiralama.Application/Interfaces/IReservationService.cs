using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Dtos.Reservation;

namespace OtoKiralama.Application.Interfaces
{
    public interface IReservationService
    {
        Task<PagedResponse<ReservationListItemDto>> GetAllReservationsAsync(int pageNumber, int pageSize);
        Task<ReservationReturnDto> GetReservationByIdAsync(int id);
        Task<int> CreateReservationAsync(ReservationCreateDto reservationCreateDto);
        Task DeleteReservationAsync(int id);
        Task CancelReservation(int id);
        Task CompleteReservation(int id);
        Task<ReservationReturnDto> GetReservationByReservationNumberAndEmail(ReservationGetByEmailAndNumberDto reservationGetByEmailAndNumberDto);
    }
}
