using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Dtos.Reservation;
using OtoKiralama.Application.Dtos.User;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Application.Services;

public class ProfileService : IProfileService
{
    public Task<UserGetDto> GetUserInformationAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteUser()
    {
        throw new NotImplementedException();
    }

    public Task<PagedResponse<ReservationListItemDto>> GetUserReservations(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task ChangeSubscribtionStatus(ChangeSubscribtionStatusDto changeSubscribtionStatusDto)
    {
        throw new NotImplementedException();
    }
}