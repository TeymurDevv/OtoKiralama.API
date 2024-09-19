using OtoKiralama.Domain.Entities.Common;

namespace OtoKiralama.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
