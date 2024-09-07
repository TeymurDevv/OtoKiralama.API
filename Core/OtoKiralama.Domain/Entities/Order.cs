using OtoKiralama.Domain.Common;

namespace OtoKiralama.Domain.Entities
{
    public class Order : BaseEntity
    {
        public double Price { get; set; }
        public string Status { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
