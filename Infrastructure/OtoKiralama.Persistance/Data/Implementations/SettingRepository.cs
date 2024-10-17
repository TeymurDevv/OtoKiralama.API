using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class SettingRepository: Repository<Setting>, ISettingRepository
    {
        private readonly AppDbContext _context;
        public SettingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> CountAsync()
        {
            return await _context.Set<Setting>().CountAsync();
        }
    }
}
