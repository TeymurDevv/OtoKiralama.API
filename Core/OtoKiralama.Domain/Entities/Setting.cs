using OtoKiralama.Domain.Entities.Common;

namespace OtoKiralama.Domain.Entities
{
    public class Setting : BaseEntity
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
