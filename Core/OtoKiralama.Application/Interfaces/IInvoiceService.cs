using OtoKiralama.Application.Dtos.IndividualInvoice;
using OtoKiralama.Application.Dtos.Invoice;
using OtoKiralama.Domain.Enums;

namespace OtoKiralama.Application.Interfaces;

public interface IInvoiceService
{
    Task<List<InvoiceReturnDto>> GetAllInvoicesAsync();
    Task CreateInvoiceAsync(InvoiceCreateDto individualInvoiceCreateDto);
    Task DeleteInvoice(int id, InvoiceType invoiceType);
}