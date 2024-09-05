using OtoKiralama.Domain.Common;

namespace OtoKiralama.Domain.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
