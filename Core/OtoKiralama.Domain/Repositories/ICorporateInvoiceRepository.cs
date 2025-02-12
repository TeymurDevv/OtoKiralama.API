using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Domain.Repositories;

public interface ICorporateInvoiceRepository : IRepository<CorporateInvoice>
{
    Task<int> CountAsync();
}