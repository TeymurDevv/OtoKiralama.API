using OtoKiralama.Domain.Entities.Common;

namespace OtoKiralama.Domain.Entities;

public class Country : BaseEntity
{
    public string Name { get; set; }
    public List<Invoice> Invoices { get; set; }
}