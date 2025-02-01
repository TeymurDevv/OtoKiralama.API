using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

            var existCompany = await _unitOfWork.CompanyRepository.isExists(c => c.Id == carCreateDto.CompanyId);
            if (!existCompany)
                throw new CustomException(404, "CompanyId", "Company not found with this Id");

            var existBrand = await _unitOfWork.BrandRepository.isExists(b => b.Id == carCreateDto.BrandId);
            if (!existBrand)
                throw new CustomException(404, "BrandId", "Brand not found with this Id");

            var existModel = await _unitOfWork.ModelRepository.isExists(m => m.Id == carCreateDto.ModelId);
            if (!existModel)
                throw new CustomException(404, "ModelId", "Model not found with this Id");

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
                throw new CustomException(400, "Plate", "Car already exists with this plate");

            var model = await _unitOfWork.ModelRepository.GetEntity(
                includes: query => query.Include(m => m.CarPhoto),
                predicate: m => m.Id == carCreateDto.ModelId);

            var carPhoto = model.CarPhoto;
            if (carPhoto == null)
                throw new CustomException(404, "CarPhotoId", "CarPhoto not found in this model");

            var car = _mapper.Map<Car>(carCreateDto);

            if (carCreateDto.Limit != null) car.SetLimit(carCreateDto.Limit.Value);  // deposit profileda set olunmalidi auto limit burda set olunur profilede seti legv elemek lazimdi ve deliverytype profileda set olunmalidi nezere alarsan bulari


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
                    .Include(c => c.Model)
                    .ThenInclude(c => c.CarPhoto)
                    .Include(c => c.Body)
                    .Include(c => c.Class)
                    .Include(c => c.Fuel)
                    .Include(c => c.Gear)
                    .Include(c => c.Location)
                    .Include(c => c.Company)
                    .Include(c => c.DeliveryType)
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
                    .Include(c => c.Model)
                    .ThenInclude(m => m.CarPhoto)
                    .Include(c => c.Body)
                    .Include(c => c.Class)
                    .Include(c => c.Fuel)
                    .Include(c => c.Gear)
                    .Include(c => c.Location)
                    .Include(c => c.Company)
                    .Include(c => c.DeliveryType)
            );

            if (car is null)
                throw new CustomException(404, "Id", "Car not found with this Id");

            return _mapper.Map<CarReturnDto>(car);
        }

        public async Task ChangeCarStatus(int id)
        {
            var car = await _unitOfWork.CarRepository.GetEntity(c => c.Id == id);
            if (car is null)
                throw new CustomException(404, "Id", "Car not found with this Id");
            car.IsActive = !car.IsActive;
            await _unitOfWork.CarRepository.Update(car);
            _unitOfWork.Commit();
        }

        public async Task MarkAsDeactıve(int id)
        {
            var car = await _unitOfWork.CarRepository.GetEntity(c => c.Id == id);
            if (car is null)
                throw new CustomException(404, "Id", "Car not found with this Id");
            car.IsActive = false;
            car.IsReserved = false;
            _unitOfWork.Commit();
        }

        public async Task<List<CarListItemDto>> GetAllFilteredCarsAsync(int pickupLocationId, int? dropoffLocationId, DateTime fromDate, DateTime toDate)
        {
            var pickupLocation = await _unitOfWork.LocationRepository.GetEntity(l => l.Id == pickupLocationId);
            if (pickupLocation is null)
                throw new CustomException(404, "Id", "PickupLocation not found");

            dropoffLocationId ??= pickupLocationId;

            var dropoffLocation = await _unitOfWork.LocationRepository.GetEntity(l => l.Id == dropoffLocationId);
            if (dropoffLocation is null)
                throw new CustomException(404, "Id", "DropoffLocation not found");

            var cars = await _unitOfWork.CarRepository.GetQuery(
                predicate: c => c.LocationId == pickupLocationId &&
                                !c.IsReserved &&
                                !c.Reservations.Any(b => (fromDate >= b.StartDate && fromDate <= b.EndDate) ||
                                                         (toDate >= b.StartDate && toDate <= b.EndDate) ||
                                                         (fromDate <= b.StartDate && toDate >= b.EndDate)),
                includes: query => query
                    .Include(c => c.Brand)
                    .Include(c => c.Model)
                    .ThenInclude(m => m.CarPhoto)
                    .Include(c => c.Body)
                    .Include(c => c.Class)
                    .Include(c => c.Fuel)
                    .Include(c => c.Gear)
                    .Include(c => c.Location)
                    .Include(c => c.Company)
            );

            return _mapper.Map<List<CarListItemDto>>(cars);
        }
    }
}
