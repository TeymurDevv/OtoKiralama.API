using AutoMapper;
using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<Brand, BrandReturnDto>();
            CreateMap<BrandCreateDto, Brand>();

        }
    }
}
