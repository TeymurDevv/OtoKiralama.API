using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly AppDbContext _context;
        public CompanyRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> CountAsync()
        {
            return await _context.Set<Company>().CountAsync();
        }
    }
}
