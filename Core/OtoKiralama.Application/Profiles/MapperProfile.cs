using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OtoKiralama.Application.Dtos.Body;
using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Application.Dtos.Car;
using OtoKiralama.Application.Dtos.Class;
using OtoKiralama.Application.Dtos.Company;
using OtoKiralama.Application.Dtos.Fuel;
using OtoKiralama.Application.Dtos.Gear;
using OtoKiralama.Application.Dtos.Location;
using OtoKiralama.Application.Dtos.Model;
using OtoKiralama.Application.Dtos.Role;
using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Brand, BrandReturnDto>();
            CreateMap<Brand,BrandListItemDto>();
            CreateMap<BrandCreateDto, Brand>();
            CreateMap<Location, LocationReturnDto>();
            CreateMap<Location, LocationListItemDto>();
            CreateMap<LocationCreateDto, Location>();
            CreateMap<IdentityRole, RoleReturnDto>();
            CreateMap<RoleCreateDto, IdentityRole>();
            CreateMap<GearCreateDto, Gear>();
            CreateMap<Gear, GearListItemDto>();
            CreateMap<Gear, GearReturnDto>();
            CreateMap<BodyCreateDto, Body>();
            CreateMap<Body, BodyReturnDto>();
            CreateMap<Body, BodyListItemDto>();
            CreateMap<FuelCreateDto, Fuel>();
            CreateMap<Fuel, FuelReturnDto>();
            CreateMap<Fuel, FuelListItemDto>();
            CreateMap<ClassCreateDto, Class>();
            CreateMap<Class, ClassReturnDto>();
            CreateMap<Class, ClassListItemDto>();
            CreateMap<Car, CarListItemDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class))
                .ForMember(dest => dest.Fuel, opt => opt.MapFrom(src => src.Fuel))
                .ForMember(dest => dest.Gear, opt => opt.MapFrom(src => src.Gear))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ReverseMap();
            CreateMap<Car, CarReturnDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class))
                .ForMember(dest => dest.Fuel, opt => opt.MapFrom(src => src.Fuel))
                .ForMember(dest => dest.Gear, opt => opt.MapFrom(src => src.Gear))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ReverseMap();
            CreateMap<CarCreateDto, Car>();
            CreateMap<CompanyCreateDto, Company>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
            CreateMap<Company, CompanyReturnDto>();
            CreateMap<Company, CompanyListItemDto>();
            CreateMap<Model, ModelReturnDto>();
            CreateMap<Model, ModelListItemDto>();
            CreateMap<ModelCreateDto, Model>();
        }
    }
}
