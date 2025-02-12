using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Domain.Repositories;

public interface ICountryRepository : IRepository<Country>
{
    Task<int> CountAsync();
}