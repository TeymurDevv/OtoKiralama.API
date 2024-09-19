using OtoKiralama.Domain.Entities.Common;

namespace OtoKiralama.Domain.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
