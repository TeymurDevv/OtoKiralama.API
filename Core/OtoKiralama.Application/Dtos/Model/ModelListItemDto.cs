using OtoKiralama.Application.Dtos.Brand;

namespace OtoKiralama.Application.Dtos.Model
{
    public class ModelListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BrandReturnDto Brand { get; set; }
    }
}
