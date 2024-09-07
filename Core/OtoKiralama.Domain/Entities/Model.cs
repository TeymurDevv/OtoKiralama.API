using OtoKiralama.Domain.Common;

namespace OtoKiralama.Domain.Entities
{
    public class Model : BaseEntity
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
