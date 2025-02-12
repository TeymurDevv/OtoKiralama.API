using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Domain.Repositories;

public interface IIndividualCompanyInvoiceRepository : IRepository<IndividualCompanyInvoice>
{
    Task<int> CountAsync();
}