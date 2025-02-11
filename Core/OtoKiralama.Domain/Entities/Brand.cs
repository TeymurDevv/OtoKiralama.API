using OtoKiralama.Domain.Entities.Common;

namespace OtoKiralama.Domain.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public List<Model> Models { get; set; }
    }
}
