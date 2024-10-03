using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.Body;
using OtoKiralama.Application.Dtos.Car;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Data.Implementations;

namespace OtoKiralama.Application.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateCarAsync(CarCreateDto carCreateDto)
        {
            var existBody = await _unitOfWork.BodyRepository.isExists(b => b.Id == carCreateDto.BodyId);
            if (!existBody)
                throw new CustomException(404, "BodyId", "Body not found with this Id");
            var existLocation = await _unitOfWork.LocationRepository.isExists(l => l.Id == carCreateDto.LocationId);
            if (!existLocation)
                throw new CustomException(404, "LocationId", "Location not found with this Id");
            var existBrand = await _unitOfWork.BrandRepository.isExists(b => b.Id == carCreateDto.BrandId);
            if (!existBrand)
                throw new CustomException(404, "BrandId", "Brand not found with this Id");
            var existGear = await _unitOfWork.GearRepository.isExists(g => g.Id == carCreateDto.GearId);
            if (!existGear)
                throw new CustomException(404, "GearId", "Gear not found with this Id");
            var existFuel = await _unitOfWork.FuelRepository.isExists(f => f.Id == carCreateDto.FuelId);
            if (!existFuel)
                throw new CustomException(404, "FuelId", "Fuel not found with this Id");
            var existClass = await _unitOfWork.ClassRepository.isExists(c => c.Id == carCreateDto.ClassId);
            if (!existClass)
                throw new CustomException(404, "ClassId", "Class not found with this Id");
            var existCar = await _unitOfWork.CarRepository.isExists(c => c.Plate == carCreateDto.Plate);
            if (existCar)
                throw new CustomException(400, "Plate", "Car already exist with this plate");
            var car = _mapper.Map<Car>(carCreateDto);
            await _unitOfWork.CarRepository.Create(car);
            _unitOfWork.Commit();
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _unitOfWork.CarRepository.GetEntity(c => c.Id == id);
            if (car is null)
                throw new CustomException(404, "Id", "Car not found with this Id");
            await _unitOfWork.CarRepository.Delete(car);
            _unitOfWork.Commit();
        }

        public async Task<PagedResponse<CarListItemDto>> GetAllCarsAsync(int pageNumber, int pageSize)
        {
            var totalCars = await _unitOfWork.CarRepository.CountAsync();
            var cars = await _unitOfWork.CarRepository.GetAll(
                includes: query => query
                    .Include(c => c.Brand)
                    .Include(c => c.Body)
                    .Include(c => c.Class)
                    .Include(c => c.Fuel)
                    .Include(c => c.Gear)
                    .Include(c => c.Location)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
            );

            return new PagedResponse<CarListItemDto>
            {
                Data = _mapper.Map<List<CarListItemDto>>(cars),
                TotalCount = totalCars,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }
        public async Task<CarReturnDto> GetCarByIdAsync(int id)
        {
            var car = await _unitOfWork.CarRepository.GetEntity(
                c => c.Id == id,
                includes: query => query
                    .Include(c => c.Brand)
                    .Include(c => c.Body)
                    .Include(c => c.Class)
                    .Include(c => c.Fuel)
                    .Include(c => c.Gear)
                    .Include(c => c.Location)
            );

            if (car is null)
                throw new CustomException(404, "Id", "Car not found with this Id");

            return _mapper.Map<CarReturnDto>(car);
        }
    }
}
