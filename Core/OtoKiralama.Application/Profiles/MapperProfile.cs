﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OtoKiralama.Application.Dtos.Body;
using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Application.Dtos.Car;
using OtoKiralama.Application.Dtos.CarPhoto;
using OtoKiralama.Application.Dtos.Class;
using OtoKiralama.Application.Dtos.Company;
using OtoKiralama.Application.Dtos.Country;
using OtoKiralama.Application.Dtos.DeliveryType;
using OtoKiralama.Application.Dtos.FilterRange;
using OtoKiralama.Application.Dtos.Fuel;
using OtoKiralama.Application.Dtos.Gear;
using OtoKiralama.Application.Dtos.IndividualInvoice;
using OtoKiralama.Application.Dtos.Invoice;
using OtoKiralama.Application.Dtos.Location;
using OtoKiralama.Application.Dtos.Model;
using OtoKiralama.Application.Dtos.Reservation;
using OtoKiralama.Application.Dtos.Role;
using OtoKiralama.Application.Dtos.Setting;
using OtoKiralama.Application.Dtos.User;
using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Brand, BrandReturnDto>();
            CreateMap<Brand, BrandListItemDto>();
            CreateMap<BrandCreateDto, Brand>();
            CreateMap<BrandUpdateDto, Brand>();
            CreateMap<Location, LocationReturnDto>();
            CreateMap<Location, LocationListItemDto>();
            CreateMap<LocationCreateDto, Location>();
            CreateMap<IdentityRole, RoleReturnDto>();
            CreateMap<RoleCreateDto, IdentityRole>();
            CreateMap<GearCreateDto, Gear>();
            CreateMap<Gear, GearListItemDto>();
            CreateMap<Gear, GearReturnDto>();
            CreateMap<BodyUpdateDto, Body>();
            CreateMap<BodyCreateDto, Body>();
            CreateMap<Body, BodyReturnDto>();
            CreateMap<Body, BodyListItemDto>();
            CreateMap<FuelCreateDto, Fuel>();
            CreateMap<Fuel, FuelReturnDto>();
            CreateMap<Fuel, FuelListItemDto>();
            CreateMap<FuelUpdateDto, Fuel>().
                ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                                    srcMember != null && (!(srcMember is string str) || !string.IsNullOrWhiteSpace(str))
                                ));
            //bu setir gedir eger  property bosdusa onu goturmuk belelikle user bos gonderse  update nezere alinmir
            CreateMap<ClassUpdateDto, Class>().
                 ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                                    srcMember != null && (!(srcMember is string str) || !string.IsNullOrWhiteSpace(str))
                                ));
            CreateMap<DeliveryTypeUpdateDto, DeliveryType>().
             ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                                srcMember != null && (!(srcMember is string str) || !string.IsNullOrWhiteSpace(str))
                            ));
            CreateMap<GearUpdateDto, Gear>().
             ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                                srcMember != null && (!(srcMember is string str) || !string.IsNullOrWhiteSpace(str))
                            ));
            CreateMap<LocationUpdateDto, Location>().
            ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                               srcMember != null && (!(srcMember is string str) || !string.IsNullOrWhiteSpace(str))
                           ));
            CreateMap<ModelUpdateDto, Model>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                               srcMember != null && (!(srcMember is string str) || !string.IsNullOrWhiteSpace(str))
                           ));

            CreateMap<ClassCreateDto, Class>();
            CreateMap<Class, ClassReturnDto>();
            CreateMap<Class, ClassListItemDto>();
            CreateMap<Car, CarListItemDto>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class))
                .ForMember(dest => dest.Fuel, opt => opt.MapFrom(src => src.Fuel))
                .ForMember(dest => dest.Gear, opt => opt.MapFrom(src => src.Gear))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForPath(dest => dest.Model.CarPhoto, opt => opt.MapFrom(src => src.Model.CarPhoto))
                .ForPath(dest => dest.Model.Brand, opt => opt.MapFrom(src => src.Model.Brand))
                .ForPath(dest => dest.DeliveryType, opt => opt.MapFrom(src => src.DeliveryType))
                .ReverseMap();
            CreateMap<Car, CarReturnDto>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class))
                .ForMember(dest => dest.Fuel, opt => opt.MapFrom(src => src.Fuel))
                .ForMember(dest => dest.Gear, opt => opt.MapFrom(src => src.Gear))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForPath(dest => dest.Model.CarPhoto, opt => opt.MapFrom(src => src.Model.CarPhoto))
                .ForPath(dest => dest.Model.Brand, opt => opt.MapFrom(src => src.Model.Brand))
                .ForPath(dest => dest.DeliveryType, opt => opt.MapFrom(src => src.DeliveryType))
                .ReverseMap();
            CreateMap<CarCreateDto, Car>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.IsReserved, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.IsLimited, opt => opt.MapFrom(src => src.Limit.HasValue))
                .ForMember(dest => dest.Limit, opt => opt.MapFrom(src => src.Limit ?? null))
                .ReverseMap();
            CreateMap<CarUpdateDto, Car>()
                .ForMember(dest => dest.IsLimited, opt => opt.MapFrom(src => src.Limit.HasValue))
                .ForMember(dest => dest.Limit, opt => opt.MapFrom(src => src.Limit ?? null))
                .ReverseMap();

            CreateMap<CompanyCreateDto, Company>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
            CreateMap<Company, CompanyReturnDto>();
            CreateMap<Company, CompanyListItemDto>();
            CreateMap<CompanyUpdateDto, Company>();
            CreateMap<CompanyFullUpdateDto, Company>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            CreateMap<Model, ModelReturnDto>();
            CreateMap<Model, ModelListItemDto>();
            CreateMap<ModelCreateDto, Model>();
            CreateMap<AppUser, UserReturnDto>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company));
            CreateMap<AppUser, UserListItemDto>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company));
            CreateMap<CarPhoto, CarPhotoReturnDto>()
               .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<CarPhoto, CarPhotoListItemDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

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
                .ForPath(dest => dest.Car.Model.CarPhoto, opt => opt.MapFrom(src => src.Car.Model.CarPhoto))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.AppUser))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<Reservation, ReservationReturnDto>()
                .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car))
                .ForPath(dest => dest.Car.Model.CarPhoto, opt => opt.MapFrom(src => src.Car.Model.CarPhoto))
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

            //Invoice mapping

            CreateMap<InvoiceUpdateDto, Invoice>()
             .ForMember(dest => dest.InvoiceType, opt => opt.MapFrom(src => src.InvoiceType));

            CreateMap<InvoiceUpdateDto, IndividualInvoice>()
                .IncludeBase<InvoiceUpdateDto, Invoice>();

            CreateMap<InvoiceUpdateDto, IndividualCompanyInvoice>()
                .IncludeBase<InvoiceUpdateDto, Invoice>();

            CreateMap<InvoiceUpdateDto, CorporateInvoice>()
                .IncludeBase<InvoiceUpdateDto, Invoice>()
                .ForMember(dest => dest.TaxCompany, opt => opt.MapFrom(src => src.TaxCompany))
                .ForMember(dest => dest.TaxNumber, opt => opt.MapFrom(src => src.TaxNumber));



            CreateMap<InvoiceCreateDto, Invoice>()
           .ForMember(dest => dest.InvoiceType, opt => opt.MapFrom(src => src.InvoiceType));

            CreateMap<InvoiceCreateDto, IndividualInvoice>().IncludeBase<InvoiceCreateDto, Invoice>();
            CreateMap<InvoiceCreateDto, IndividualCompanyInvoice>().IncludeBase<InvoiceCreateDto, Invoice>();
            CreateMap<InvoiceCreateDto, CorporateInvoice>()
                .IncludeBase<InvoiceCreateDto, Invoice>()
                .ForMember(dest => dest.TaxCompany, opt => opt.MapFrom(src => src.TaxCompany))
                .ForMember(dest => dest.TaxNumber, opt => opt.MapFrom(src => src.TaxNumber));


            CreateMap<Invoice, InvoiceReturnDto>().ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));

            CreateMap<IndividualInvoice, InvoiceReturnDto>().IncludeBase<Invoice, InvoiceReturnDto>();
            CreateMap<IndividualCompanyInvoice, InvoiceReturnDto>().IncludeBase<Invoice, InvoiceReturnDto>();
            CreateMap<CorporateInvoice, InvoiceReturnDto>()
                .IncludeBase<Invoice, InvoiceReturnDto>()
                .ForMember(dest => dest.TaxCompany, opt => opt.MapFrom(src => src.TaxCompany))
                .ForMember(dest => dest.TaxNumber, opt => opt.MapFrom(src => src.TaxNumber));

            //country mapping
            CreateMap<CountryCreateDto, Country>();
            CreateMap<Country, CountryListItemDto>();
            CreateMap<CountryUpdateDto, Country>();
            CreateMap<Country, CountryReturnDto>();

            //Filter Range mapping
            CreateMap<FilterRangeCreateDto, FilterRange>();
            CreateMap<FilterRange, FilterRangeReturnDto>();
            CreateMap<FilterRangeUpdateDto, FilterRange>();
        }
    }
}
