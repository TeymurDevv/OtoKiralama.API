using OtoKiralama.Application.Dtos.Model;

namespace OtoKiralama.Application.Dtos.CarPhoto
{
    public class CarPhotoReturnDto
    {
        public ModelReturnDto Model { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
