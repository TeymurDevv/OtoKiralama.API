using OtoKiralama.Application.Dtos.IndividualInvoice;
using OtoKiralama.Application.Dtos.Invoice;

namespace OtoKiralama.Application.Interfaces;

public interface IInvoiceService
{
    Task<List<InvoiceReturnDto>> GetAllInvoicesAsync();
    Task CreateInvoiceAsync(InvoiceCreateDto individualInvoiceCreateDto);
}