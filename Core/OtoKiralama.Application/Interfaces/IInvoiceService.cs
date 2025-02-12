using OtoKiralama.Application.Dtos.IndividualInvoice;
using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Application.Interfaces;

public interface IInvoiceService
{
    Task<List<IndividualInvoice>> GetAllInvoicesAsync();
    Task CreateInvoiceAsync(IndividualInvoiceCreateDto individualInvoiceCreateDto);
}