using OtoKiralama.Domain.Entities.Common;

namespace OtoKiralama.Domain.Entities
{
    public class CarPhoto : BaseEntity
    {
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public string ImageUrl { get; set; }
    }
}
