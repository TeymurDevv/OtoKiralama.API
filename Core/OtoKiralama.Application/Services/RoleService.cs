using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.Role;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleReturnDto>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return _mapper.Map<List<RoleReturnDto>>(roles);
        }
        public async Task<RoleReturnDto> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                throw new CustomException(404, "Id", "Role not found with this Id");
            return _mapper.Map<RoleReturnDto>(role);
        }
    }
}
