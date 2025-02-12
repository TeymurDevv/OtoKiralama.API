using OtoKiralama.Domain.Entities.Common;
using OtoKiralama.Domain.Enums;

namespace OtoKiralama.Domain.Entities;

public abstract class Invoice : BaseEntity
{
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public string Title { get; set; }
    public string Address { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public string City { get; set; }
    public string Area { get; set; }
    public string PostalCode { get; set; }
    public InvoiceType InvoiceType { get; set; }
}