using OtoKiralama.Domain.Entities.Common;
using OtoKiralama.Domain.Enums;

namespace OtoKiralama.Domain.Entities
{
    public class FilterRange : BaseEntity
    {
        public int Id { get; set; }
        public FilterRangeType Type { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}
