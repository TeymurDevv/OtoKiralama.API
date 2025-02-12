using System.Data.Entity;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations;

public class CountryRepository : Repository<Country>, ICountryRepository
{
    private readonly AppDbContext _context;
    public CountryRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<int> CountAsync()
    {
        return await _context.Set<Country>().CountAsync();
    }
}