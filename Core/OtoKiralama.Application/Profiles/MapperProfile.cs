using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OtoKiralama.Application.Dtos.Body;
using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Application.Dtos.Car;
using OtoKiralama.Application.Dtos.CarPhoto;
using OtoKiralama.Application.Dtos.Class;
using OtoKiralama.Application.Dtos.Company;
using OtoKiralama.Application.Dtos.DeliveryType;
using OtoKiralama.Application.Dtos.Fuel;
using OtoKiralama.Application.Dtos.Gear;
using OtoKiralama.Application.Dtos.Location;
using OtoKiralama.Application.Dtos.Model;
using OtoKiralama.Application.Dtos.Reservation;
using OtoKiralama.Application.Dtos.Role;
using OtoKiralama.Application.Dtos.Setting;
using OtoKiralama.Application.Dtos.User;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Entities;

namespace OtoKiralama.Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Brand, BrandReturnDto>();
            CreateMap<Brand, BrandListItemDto>();
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
                //.ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class))
                .ForMember(dest => dest.Fuel, opt => opt.MapFrom(src => src.Fuel))
                .ForMember(dest => dest.Gear, opt => opt.MapFrom(src => src.Gear))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.CarPhoto, opt => opt.MapFrom(src => src.Model.CarPhoto))
                .ReverseMap();
            CreateMap<Car, CarReturnDto>()
                //.ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class))
                .ForMember(dest => dest.Fuel, opt => opt.MapFrom(src => src.Fuel))
                .ForMember(dest => dest.Gear, opt => opt.MapFrom(src => src.Gear))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.CarPhoto, opt => opt.MapFrom(src => src.Model.CarPhoto))
                .ReverseMap();
            CreateMap<CarCreateDto, Car>();
            CreateMap<CompanyCreateDto, Company>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
            CreateMap<Company, CompanyReturnDto>();
            CreateMap<Company, CompanyListItemDto>();
            CreateMap<Model, ModelReturnDto>();
            CreateMap<Model, ModelListItemDto>();
            CreateMap<ModelCreateDto, Model>();
            CreateMap<AppUser, UserReturnDto>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company));
            CreateMap<AppUser, UserListItemDto>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company));
            CreateMap<CarPhoto, CarPhotoReturnDto>()
               .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
               .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model));

            CreateMap<CarPhoto, CarPhotoListItemDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model));

            CreateMap<CarPhotoCreateDto, CarPhoto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            CreateMap<Setting, SettingReturnDto>();
            CreateMap<Setting, SettingListItemDto>();
            CreateMap<SettingCreateDto, Setting>();
            CreateMap<SettingUpdateDto, Setting>();
            CreateMap<CarPhoto, CarPhotoReturnDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
            CreateMap<Reservation, ReservationListItemDto>()
                .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car))
                .ForPath(dest => dest.Car.CarPhoto, opt => opt.MapFrom(src => src.Car.Model.CarPhoto))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.AppUser))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<Reservation, ReservationReturnDto>()
                .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car))
                .ForPath(dest => dest.Car.CarPhoto, opt => opt.MapFrom(src => src.Car.Model.CarPhoto))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.AppUser))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
            CreateMap<ReservationCreateDto, Reservation>();
            CreateMap<AppUser, UserGetDto>();
            CreateMap<UpdateUserDto, AppUser>()
            .ForAllMembers(options => options.Condition((src, dest, srcMember) => srcMember != null));

            //DeliveryType Mapping

            CreateMap<DeliveryType, DeliveryTypeReturnDto>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<DeliveryType, DeliveryTypeListItemDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<DeliveryTypeCreateDto, DeliveryType>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        }
    }
}
