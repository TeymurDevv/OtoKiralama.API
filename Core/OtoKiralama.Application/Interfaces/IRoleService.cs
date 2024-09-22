using OtoKiralama.Application.Dtos.Role;

namespace OtoKiralama.Application.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleReturnDto>> GetAllRolesAsync();
        Task<RoleReturnDto> GetRoleByIdAsync(string id);
    }
}
