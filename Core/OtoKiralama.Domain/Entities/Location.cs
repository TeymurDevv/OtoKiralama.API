using OtoKiralama.Domain.Entities.Common;

namespace OtoKiralama.Domain.Entities
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
