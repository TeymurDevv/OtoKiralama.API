using System.Data.Entity;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations;

public class IndividualCompanyInvoiceRepository : Repository<IndividualCompanyInvoice>, IIndividualCompanyInvoiceRepository
{
    private readonly AppDbContext _context;
    public IndividualCompanyInvoiceRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<int> CountAsync()
    {
        return await _context.Set<Country>().CountAsync();
    }
}