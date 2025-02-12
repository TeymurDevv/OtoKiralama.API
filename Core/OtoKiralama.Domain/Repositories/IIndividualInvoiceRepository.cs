using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Domain.Repositories;

public interface IIndividualInvoiceRepository : IRepository<IndividualInvoice>
{
    Task<int> CountAsync();
}