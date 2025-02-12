namespace OtoKiralama.Domain.Entities;

public class CorporateInvoice : Invoice
{
    public string TaxCompany { get; set; }
    public string TaxNumber { get; set; }
}