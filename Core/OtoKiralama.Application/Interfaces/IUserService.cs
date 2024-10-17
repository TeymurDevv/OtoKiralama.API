using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Dtos.User;

namespace OtoKiralama.Application.Interfaces
{
    public interface IUserService
    {
        Task <PagedResponse<UserListItemDto>> GetAllUsers(int pageNumber, int pageSize);
         Task<UserReturnDto> GetUserById(string userId);
        //IEnumerable<User> GetAllCompanyUsers(int companyId);
        //IEnumerable<User> GetAllCompanyAdmins(int companyId);
        //IEnumerable<User> GetAllCompanyAgents(int companyId);
        //IEnumerable<User> GetUsersByCompanyId(int companyId);
        Task DeleteUser(string userId);
    }
}
