using OtoKiralama.Domain.Entities.Common;

namespace OtoKiralama.Domain.Entities
{
    public class DeliveryType : BaseEntity
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set; }
    }
}
