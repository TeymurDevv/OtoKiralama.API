using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Application.Dtos.CarPhoto;

namespace OtoKiralama.Application.Dtos.Model
{
    public class ModelReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BrandReturnDto Brand { get; set; }
        public CarPhotoReturnDto CarPhoto { get; set; }
    }
}
