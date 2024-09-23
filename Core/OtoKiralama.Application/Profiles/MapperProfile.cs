using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Application.Dtos.Gear;
using OtoKiralama.Application.Dtos.Location;
using OtoKiralama.Application.Dtos.Role;
using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Brand, BrandReturnDto>();
            CreateMap<BrandCreateDto, Brand>();
            CreateMap<Location, LocationReturnDto>();
            CreateMap<LocationCreateDto, Location>();
            CreateMap<IdentityRole, RoleReturnDto>();
            CreateMap<RoleCreateDto, IdentityRole>();
            CreateMap<GearCreateDto, Gear>();
            CreateMap<Gear, GearReturnDto>();
        }
    }
}
