using OtoKiralama.Domain.Enums;

namespace OtoKiralama.Application.Dtos.FilterRange
{
    public class FilterRangeReturnDto
    {
        public int Id { get; set; }
        public FilterRangeType Type { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}
