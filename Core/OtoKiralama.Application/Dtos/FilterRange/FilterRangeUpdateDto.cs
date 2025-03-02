using OtoKiralama.Domain.Enums;

namespace OtoKiralama.Application.Dtos.FilterRange
{
    public class FilterRangeUpdateDto
    {
        public FilterRangeType Type { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}
