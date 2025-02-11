using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Dtos.Reservation;
using OtoKiralama.Application.Dtos.User;

namespace OtoKiralama.Application.Interfaces;

public interface IProfileService
{
    Task<UserGetDto> GetUserInformationAsync();
    Task DeleteUser();
    Task<PagedResponse<ReservationListItemDto>> GetUserReservations(int pageNumber, int pageSize);
    Task ChangeSubscribtionStatus(ChangeSubscribtionStatusDto changeSubscribtionStatusDto);
}