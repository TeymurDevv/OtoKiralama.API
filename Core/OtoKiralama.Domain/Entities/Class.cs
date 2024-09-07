using OtoKiralama.Domain.Common;

namespace OtoKiralama.Domain.Entities
{
    public class Class : BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
