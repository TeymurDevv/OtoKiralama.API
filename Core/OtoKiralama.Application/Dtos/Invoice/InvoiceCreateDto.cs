using OtoKiralama.Domain.Enums;

namespace OtoKiralama.Application.Dtos.IndividualInvoice;

public class InvoiceCreateDto
{
    public string Title { get; set; }
    public string Address { get; set; }
    public int CountryId { get; set; }
    public string City { get; set; }
    public string Area { get; set; }
    public string PostalCode { get; set; }
    public InvoiceType InvoiceType { get; set; }
    public string? TaxCompany { get; set; }
    public string? TaxNumber { get; set; }
}