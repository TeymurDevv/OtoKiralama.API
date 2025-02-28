using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.Car;
using OtoKiralama.Application.Dtos.Car.CarSearchDtos;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Enums;
using OtoKiralama.Domain.Repositories;
using ZiggyCreatures.Caching.Fusion;

namespace OtoKiralama.Application.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFusionCache _fusionCache;
        private readonly UserManager<AppUser> _userManager;

        public CarService(IUnitOfWork unitOfWork, IMapper mapper, IFusionCache fusionCache, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fusionCache = fusionCache;
            _userManager = userManager;
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

            var existDeliveryType = await _unitOfWork.DeliveryTypeRepository.isExists(d => d.Id == carCreateDto.DeliveryTypeId);
            if (!existDeliveryType)
                throw new CustomException(404, "DeliveryTypeId", "Delivery type not found with this Id");

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
            await _unitOfWork.CarRepository.Create(car);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task UpdateCarAsync(int id, string userId, CarUpdateDto carUpdateDto)
        {
            var existUser = await _userManager.FindByIdAsync(userId);
            if (existUser == null)
                throw new CustomException(404, "UserId", "User not found");

            if (existUser.Company == null)
                throw new CustomException(404, "Id", "Company not found with this Id");

            var car = await _unitOfWork.CarRepository.GetEntity(c => c.Id == id);
            if (car is null)
                throw new CustomException(404, "Id", "Car not found with this Id");

            if (existUser.CompanyId != car.CompanyId)
                throw new CustomException(404, "Company", "This company no access for update this car");

            if (!await _unitOfWork.BodyRepository.isExists(b => b.Id == carUpdateDto.BodyId))
                throw new CustomException(404, "BodyId", "Body not found with this Id");

            if (!await _unitOfWork.LocationRepository.isExists(l => l.Id == carUpdateDto.LocationId))
                throw new CustomException(404, "LocationId", "Location not found with this Id");

            if (!await _unitOfWork.CompanyRepository.isExists(c => c.Id == carUpdateDto.CompanyId))
                throw new CustomException(404, "CompanyId", "Company not found with this Id");

            if (!await _unitOfWork.DeliveryTypeRepository.isExists(d => d.Id == carUpdateDto.DeliveryTypeId))
                throw new CustomException(404, "DeliveryTypeId", "Delivery type not found with this Id");

            if (!await _unitOfWork.ModelRepository.isExists(m => m.Id == carUpdateDto.ModelId))
                throw new CustomException(404, "ModelId", "Model not found with this Id");

            if (!await _unitOfWork.GearRepository.isExists(g => g.Id == carUpdateDto.GearId))
                throw new CustomException(404, "GearId", "Gear not found with this Id");

            if (!await _unitOfWork.FuelRepository.isExists(f => f.Id == carUpdateDto.FuelId))
                throw new CustomException(404, "FuelId", "Fuel not found with this Id");

            if (!await _unitOfWork.ClassRepository.isExists(c => c.Id == carUpdateDto.ClassId))
                throw new CustomException(404, "ClassId", "Class not found with this Id");

            var existCarWithSamePlate = await _unitOfWork.CarRepository.isExists(c => c.Plate == carUpdateDto.Plate && c.Id != id);
            if (existCarWithSamePlate)
                throw new CustomException(400, "Plate", "Another car already exists with this plate");

            _mapper.Map(carUpdateDto, car);

            await _unitOfWork.CarRepository.Update(car);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int id, string userId)
        {
            var existUser = await _userManager.FindByIdAsync(userId);
            if (existUser == null)
                throw new CustomException(404, "UserId", "User not found");

            if (existUser.Company == null)
                throw new CustomException(404, "Id", "Company not found with this Id");

            var car = await _unitOfWork.CarRepository.GetEntity(c => c.Id == id);
            if (car is null)
                throw new CustomException(404, "Id", "Car not found with this Id");
            if (existUser.CompanyId != car.CompanyId)
                throw new CustomException(404, "Company", "This company no access for delete this car");

            await _unitOfWork.CarRepository.Delete(car);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task<PagedResponse<CarListItemDto>> GetAllCarsAsync(int pageNumber, int pageSize)
        {
            var totalCars = await _unitOfWork.CarRepository.CountAsync();
            var cars = await _unitOfWork.CarRepository.GetAll(
                includes: query => query
                    .Include(c => c.Model)
                    .ThenInclude(m => m.Brand)
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
                    .Include(c => c.Model)
                    .ThenInclude(m => m.Brand)
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
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task MarkAsDeactıve(int id)
        {
            var car = await _unitOfWork.CarRepository.GetEntity(c => c.Id == id);
            if (car is null)
                throw new CustomException(404, "Id", "Car not found with this Id");
            car.IsActive = false;
            car.IsReserved = false;
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task<List<CarListItemDto>> GetAllFilteredCarsAsync(
            int pickupLocationId, int? dropoffLocationId, DateTime fromDate, DateTime toDate)
        {
            var cacheKey = $"cars-{pickupLocationId}-{dropoffLocationId}-{fromDate:yyyyMMdd}-{toDate:yyyyMMdd}";

            var cachedCars = await _fusionCache.GetOrDefaultAsync<List<CarListItemDto>>(cacheKey);
            if (cachedCars is not null)
                return cachedCars;

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
                .Include(c => c.Model)
                    .ThenInclude(m => m.Brand)
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

            var pagedResponse = _mapper.Map<List<CarListItemDto>>(cars);

            await _fusionCache.SetAsync(cacheKey, pagedResponse, TimeSpan.FromMinutes(2));

            return pagedResponse;
        }



        public async Task<CarSearchResultDto> SearchCarsAsync(CarSearchListDto carSearchListDto)
        {
            var pickupLocation = await _unitOfWork.LocationRepository.GetEntity(l => l.Id == carSearchListDto.PickupLocationId);
            if (pickupLocation is null)
                throw new CustomException(404, "Id", "PickupLocation not found");

            carSearchListDto.DropoffLocationId ??= pickupLocation.Id;

            var dropoffLocation = await _unitOfWork.LocationRepository.GetEntity(l => l.Id == carSearchListDto.DropoffLocationId);
            if (dropoffLocation is null)
                throw new CustomException(404, "Id", "DropoffLocation not found");

            var mainQuery = await _unitOfWork.CarRepository.GetQuery(
                predicate: c => c.LocationId == pickupLocation.Id &&
                                !c.IsReserved &&
                                !c.Reservations.Any(b => (carSearchListDto.FromDate >= b.StartDate && carSearchListDto.FromDate <= b.EndDate) ||
                                                         (carSearchListDto.ToDate >= b.StartDate && carSearchListDto.ToDate <= b.EndDate) ||
                                                         (carSearchListDto.FromDate <= b.StartDate && carSearchListDto.ToDate >= b.EndDate)),
                includes: query => query
                .Include(c => c.Model)
                    .ThenInclude(m => m.Brand)
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

            mainQuery = mainQuery.Where(c => c.IsActive && !c.IsReserved);

            var originalQuery = mainQuery;

            if (carSearchListDto.BrandIds?.Any() == true)
                mainQuery = mainQuery.Where(c => carSearchListDto.BrandIds.Contains(c.Model.Brand.Id));

            if (carSearchListDto.ModelIds?.Any() == true)
                mainQuery = mainQuery.Where(c => carSearchListDto.ModelIds.Contains(c.Model.Id));

            if (carSearchListDto.GearIds?.Any() == true)
                mainQuery = mainQuery.Where(c => carSearchListDto.GearIds.Contains(c.GearId));

            if (carSearchListDto.CompanyIds?.Any() == true)
                mainQuery = mainQuery.Where(c => carSearchListDto.CompanyIds.Contains(c.CompanyId));

            if (carSearchListDto.FuelIds?.Any() == true)
                mainQuery = mainQuery.Where(c => carSearchListDto.FuelIds.Contains(c.FuelId));

            if (carSearchListDto.DeliveryTypeIds?.Any() == true)
                mainQuery = mainQuery.Where(c => carSearchListDto.DeliveryTypeIds.Contains(c.DeliveryTypeId));

            if (carSearchListDto.SeatCounts?.Any() == true)
                mainQuery = mainQuery.Where(c => carSearchListDto.SeatCounts.Contains(c.SeatCount));

            if (carSearchListDto.ClassIds?.Any() == true)
                mainQuery = mainQuery.Where(c => carSearchListDto.ClassIds.Contains(c.ClassId));

            if (carSearchListDto.LimitRangeId.HasValue)
            {
                var limitRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == carSearchListDto.LimitRangeId.Value);
                if (limitRange is not null)
                {
                    mainQuery = mainQuery.Where(c => c.Limit >= limitRange.MinValue && c.Limit <= limitRange.MaxValue);
                }
            }

            if (carSearchListDto.DailyPriceRangeId.HasValue)
            {
                var priceRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == carSearchListDto.DailyPriceRangeId.Value);
                if (priceRange is not null)
                {
                    mainQuery = mainQuery.Where(c => c.DailyPrice >= priceRange.MinValue && c.DailyPrice <= priceRange.MaxValue);
                }
            }

            if (carSearchListDto.DepositAmountRangeId.HasValue)
            {
                var depositRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == carSearchListDto.DepositAmountRangeId.Value);
                if (depositRange is not null)
                {
                    mainQuery = mainQuery.Where(c => c.DepositAmount >= depositRange.MinValue && c.DepositAmount <= depositRange.MaxValue);
                }
            }

            var cars = await mainQuery.ToListAsync();
            var mappedCars = _mapper.Map<List<CarListItemDto>>(cars);

            var result = new CarSearchResultDto
            {
                Cars = mappedCars,
                FilterCounts = await CalculateFilterCountsAsync(carSearchListDto, originalQuery)
            };

            return result;
        }
        private async Task<CarFilterCountsDto> CalculateFilterCountsAsync(CarSearchListDto dto, IQueryable<Car> query)
        {
            var filterCounts = new CarFilterCountsDto
            {
                GearCounts = await CalculateGearCountsAsync(dto, query),
                CompanyCounts = await CalculateCompanyCountsAsync(dto, query),
                FuelCounts = await CalculateFuelCountsAsync(dto, query),
                BrandCounts = await CalculateBrandCountsAsync(dto, query),
                ModelCounts = await CalculateModelCountsAsync(dto, query),
                DeliveryTypeCounts = await CalculateDeliveryTypeCountsAsync(dto, query),
                ClassCounts = await CaculateClasCountsAsync(dto, query),

                DepositRangeCounts = await CalculateRangeCountsAsync(dto, query, FilterRangeType.Deposit),
                LimitRangeCounts = await CalculateRangeCountsAsync(dto, query, FilterRangeType.Limit),
                DailyPriceRangeCounts = await CalculateRangeCountsAsync(dto, query, FilterRangeType.Price)
            };

            return filterCounts;
        }
        private async Task<List<FilterCountDto>> CalculateGearCountsAsync(CarSearchListDto dto, IQueryable<Car> query)
        {

            // Brand
            if (dto.BrandIds?.Any() == true)
                query = query.Where(c => dto.BrandIds.Contains(c.Model.Brand.Id));
            // Class
            if (dto.ClassIds?.Any() == true)
                query = query.Where(c => dto.ClassIds.Contains(c.ClassId));
            // Model
            if (dto.ModelIds?.Any() == true)
                query = query.Where(c => dto.ModelIds.Contains(c.Model.Id));
            // Company
            if (dto.CompanyIds?.Any() == true)
                query = query.Where(c => dto.CompanyIds.Contains(c.CompanyId));
            // Fuel
            if (dto.FuelIds?.Any() == true)
                query = query.Where(c => dto.FuelIds.Contains(c.FuelId));
            // DeliveryType
            if (dto.DeliveryTypeIds?.Any() == true)
                query = query.Where(c => dto.DeliveryTypeIds.Contains(c.DeliveryTypeId));
            // SeatCounts
            if (dto.SeatCounts?.Any() == true)
                query = query.Where(c => dto.SeatCounts.Contains(c.SeatCount));
            // Range-lər
            if (dto.LimitRangeId.HasValue)
            {
                var limitRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.LimitRangeId.Value);
                if (limitRange != null)
                {
                    query = query.Where(c => c.Limit >= limitRange.MinValue && c.Limit <= limitRange.MaxValue);
                }
            }
            if (dto.DailyPriceRangeId.HasValue)
            {
                var priceRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.DailyPriceRangeId.Value);
                if (priceRange != null)
                {
                    query = query.Where(c => c.DailyPrice >= priceRange.MinValue && c.DailyPrice <= priceRange.MaxValue);
                }
            }
            if (dto.DepositAmountRangeId.HasValue)
            {
                var depositRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.DepositAmountRangeId.Value);
                if (depositRange != null)
                {
                    query = query.Where(c => c.DepositAmount >= depositRange.MinValue && c.DepositAmount <= depositRange.MaxValue);
                }
            }

            // 3) Gear-ları groupby edirik, saylarını götürürük
            var gearCounts = await query
                .GroupBy(c => c.GearId)
                .Select(g => new FilterCountDto
                {
                    Id = g.Key,
                    Name = g.First().Gear.Name,
                    Count = g.Count()
                })
                .ToListAsync();

            return gearCounts;
        }
        private async Task<List<FilterCountDto>> CaculateClasCountsAsync(CarSearchListDto dto, IQueryable<Car> query)
        {

            // Gear
            if (dto.GearIds?.Any() == true)
                query = query.Where(c => dto.GearIds.Contains(c.GearId));
            // Brand
            if (dto.BrandIds?.Any() == true)
                query = query.Where(c => dto.BrandIds.Contains(c.Model.Brand.Id));
            // Model
            if (dto.ModelIds?.Any() == true)
                query = query.Where(c => dto.ModelIds.Contains(c.Model.Id));
            // Company
            if (dto.CompanyIds?.Any() == true)
                query = query.Where(c => dto.CompanyIds.Contains(c.CompanyId));
            // Fuel
            if (dto.FuelIds?.Any() == true)
                query = query.Where(c => dto.FuelIds.Contains(c.FuelId));
            // DeliveryType
            if (dto.DeliveryTypeIds?.Any() == true)
                query = query.Where(c => dto.DeliveryTypeIds.Contains(c.DeliveryTypeId));
            // SeatCounts
            if (dto.SeatCounts?.Any() == true)
                query = query.Where(c => dto.SeatCounts.Contains(c.SeatCount));
            // Range-lər
            if (dto.LimitRangeId.HasValue)
            {
                var limitRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.LimitRangeId.Value);
                if (limitRange != null)
                {
                    query = query.Where(c => c.Limit >= limitRange.MinValue && c.Limit <= limitRange.MaxValue);
                }
            }
            if (dto.DailyPriceRangeId.HasValue)
            {
                var priceRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.DailyPriceRangeId.Value);
                if (priceRange != null)
                {
                    query = query.Where(c => c.DailyPrice >= priceRange.MinValue && c.DailyPrice <= priceRange.MaxValue);
                }
            }
            if (dto.DepositAmountRangeId.HasValue)
            {
                var depositRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.DepositAmountRangeId.Value);
                if (depositRange != null)
                {
                    query = query.Where(c => c.DepositAmount >= depositRange.MinValue && c.DepositAmount <= depositRange.MaxValue);
                }
            }

            // 3) Gear-ları groupby edirik, saylarını götürürük
            var classCounts = await query
                .GroupBy(c => c.ClassId)
                .Select(g => new FilterCountDto
                {
                    Id = g.Key,
                    Name = g.First().Class.Name,
                    Count = g.Count()
                })
                .ToListAsync();

            return classCounts;
        }
        private async Task<List<FilterCountDto>> CalculateBrandCountsAsync(CarSearchListDto dto, IQueryable<Car> query)
        {


            // Class
            if (dto.ClassIds?.Any() == true)
                query = query.Where(c => dto.ClassIds.Contains(c.ClassId));
            // Gear
            if (dto.GearIds?.Any() == true)
                query = query.Where(c => dto.GearIds.Contains(c.GearId));
            // Model
            if (dto.ModelIds?.Any() == true)
                query = query.Where(c => dto.ModelIds.Contains(c.Model.Id));
            // Company
            if (dto.CompanyIds?.Any() == true)
                query = query.Where(c => dto.CompanyIds.Contains(c.CompanyId));
            // Fuel
            if (dto.FuelIds?.Any() == true)
                query = query.Where(c => dto.FuelIds.Contains(c.FuelId));
            // DeliveryType
            if (dto.DeliveryTypeIds?.Any() == true)
                query = query.Where(c => dto.DeliveryTypeIds.Contains(c.DeliveryTypeId));
            // SeatCounts
            if (dto.SeatCounts?.Any() == true)
                query = query.Where(c => dto.SeatCounts.Contains(c.SeatCount));

            // Range-lər (limit, deposit, dailyPrice)
            // ... eynilə yuxarıdakı kimi

            // BRAND-ı query-dən çıxarırıq! (dto.BrandIds tətbiq etmirik!)
            // Amma gerçəkdə "brand" filterini TƏTBİQ ETMİRİK. Yəni brand-ları "faceted" hesablamaq üçün
            // brand filtri olmadan query edirik, qalan filtrlər saxlanılır.

            var brandCounts = await query
                .GroupBy(c => c.Model.Brand.Id)
                .Select(g => new FilterCountDto
                {
                    Id = g.Key,
                    Name = g.First().Model.Brand.Name,
                    Count = g.Count()
                })
                .ToListAsync();

            return brandCounts;
        }
        private async Task<List<FilterRangeCountDto>> CalculateRangeCountsAsync(CarSearchListDto dto, IQueryable<Car> query, FilterRangeType type)
        {

            if (dto.ClassIds?.Any() == true)
                query = query.Where(c => dto.ClassIds.Contains(c.ClassId));
            // brand
            if (dto.BrandIds?.Any() == true)
                query = query.Where(c => dto.BrandIds.Contains(c.Model.Brand.Id));
            // model
            if (dto.ModelIds?.Any() == true)
                query = query.Where(c => dto.ModelIds.Contains(c.Model.Id));
            // gear
            if (dto.GearIds?.Any() == true)
                query = query.Where(c => dto.GearIds.Contains(c.GearId));
            // company
            if (dto.CompanyIds?.Any() == true)
                query = query.Where(c => dto.CompanyIds.Contains(c.CompanyId));
            // fuel
            if (dto.FuelIds?.Any() == true)
                query = query.Where(c => dto.FuelIds.Contains(c.FuelId));
            // deliveryType
            if (dto.DeliveryTypeIds?.Any() == true)
                query = query.Where(c => dto.DeliveryTypeIds.Contains(c.DeliveryTypeId));
            // seatCounts
            if (dto.SeatCounts?.Any() == true)
                query = query.Where(c => dto.SeatCounts.Contains(c.SeatCount));

            // Digər range-lər
            // Məsələn, əgər type = Deposit, depositRangeId-i TƏTBİQ ETMİRİK,
            // Amma limitRangeId, dailyPriceRangeId varsa, tətbiq edirik.

            if (type != FilterRangeType.Limit && dto.LimitRangeId.HasValue)
            {
                var limitRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.LimitRangeId.Value);
                if (limitRange != null)
                {
                    query = query.Where(c => c.Limit >= limitRange.MinValue && c.Limit <= limitRange.MaxValue);
                }
            }
            if (type != FilterRangeType.Price && dto.DailyPriceRangeId.HasValue)
            {
                var priceRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.DailyPriceRangeId.Value);
                if (priceRange != null)
                {
                    query = query.Where(c => c.DailyPrice >= priceRange.MinValue && c.DailyPrice <= priceRange.MaxValue);
                }
            }
            if (type != FilterRangeType.Deposit && dto.DepositAmountRangeId.HasValue)
            {
                var depositRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.DepositAmountRangeId.Value);
                if (depositRange != null)
                {
                    query = query.Where(c => c.DepositAmount >= depositRange.MinValue && c.DepositAmount <= depositRange.MaxValue);
                }
            }

            // 3) İndi bazadakı FilterRange-ləri götürüb, HƏR range intervalı üçün count hesablayaq
            var filterRangesQuery = await _unitOfWork.FilterRangeRepository
                .GetQuery(r => r.Type == type);
            var filterRanges = await filterRangesQuery.ToListAsync();

            var result = new List<FilterRangeCountDto>();
            foreach (var range in filterRanges)
            {
                // Hər interval üçün Count
                int count = await query.CountAsync(c =>
                    (type == FilterRangeType.Deposit && c.DepositAmount >= range.MinValue && c.DepositAmount <= range.MaxValue) ||
                    (type == FilterRangeType.Limit && c.Limit >= range.MinValue && c.Limit <= range.MaxValue) ||
                    (type == FilterRangeType.Price && c.DailyPrice >= range.MinValue && c.DailyPrice <= range.MaxValue)
                );

                result.Add(new FilterRangeCountDto
                {
                    Id = range.Id,
                    Name = $"{range.MinValue}-{range.MaxValue} ₺",
                    Count = count
                });
            }

            return result;
        }
        private async Task<List<FilterCountDto>> CalculateCompanyCountsAsync(CarSearchListDto dto, IQueryable<Car> query)
        {

            // Class
            if (dto.ClassIds?.Any() == true)
                query = query.Where(c => dto.ClassIds.Contains(c.ClassId));
            // Gear
            if (dto.GearIds?.Any() == true)
                query = query.Where(c => dto.GearIds.Contains(c.GearId));
            // Fuel
            if (dto.FuelIds?.Any() == true)
                query = query.Where(c => dto.FuelIds.Contains(c.FuelId));
            // Brand
            if (dto.BrandIds?.Any() == true)
                query = query.Where(c => dto.BrandIds.Contains(c.Model.Brand.Id));
            // Model
            if (dto.ModelIds?.Any() == true)
                query = query.Where(c => dto.ModelIds.Contains(c.Model.Id));
            // DeliveryType
            if (dto.DeliveryTypeIds?.Any() == true)
                query = query.Where(c => dto.DeliveryTypeIds.Contains(c.DeliveryTypeId));
            // SeatCounts
            if (dto.SeatCounts?.Any() == true)
                query = query.Where(c => dto.SeatCounts.Contains(c.SeatCount));

            // Range-lər
            if (dto.LimitRangeId.HasValue)
            {
                var limitRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.LimitRangeId.Value);
                if (limitRange != null)
                {
                    query = query.Where(c => c.Limit >= limitRange.MinValue && c.Limit <= limitRange.MaxValue);
                }
            }
            if (dto.DailyPriceRangeId.HasValue)
            {
                var priceRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.DailyPriceRangeId.Value);
                if (priceRange != null)
                {
                    query = query.Where(c => c.DailyPrice >= priceRange.MinValue && c.DailyPrice <= priceRange.MaxValue);
                }
            }
            if (dto.DepositAmountRangeId.HasValue)
            {
                var depositRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.DepositAmountRangeId.Value);
                if (depositRange != null)
                {
                    query = query.Where(c => c.DepositAmount >= depositRange.MinValue && c.DepositAmount <= depositRange.MaxValue);
                }
            }

            // GroupBy c => c.CompanyId
            var companyCounts = await query
                .GroupBy(c => c.CompanyId)
                .Select(g => new FilterCountDto
                {
                    Id = g.Key,
                    Name = g.First().Company.Name,
                    Count = g.Count()
                })
                .ToListAsync();

            return companyCounts;
        }
        private async Task<List<FilterCountDto>> CalculateFuelCountsAsync(CarSearchListDto dto, IQueryable<Car> query)
        {

            if (dto.ClassIds?.Any() == true)
                query = query.Where(c => dto.ClassIds.Contains(c.ClassId));

            if (dto.GearIds?.Any() == true)
                query = query.Where(c => dto.GearIds.Contains(c.GearId));

            if (dto.CompanyIds?.Any() == true)
                query = query.Where(c => dto.CompanyIds.Contains(c.CompanyId));

            if (dto.BrandIds?.Any() == true)
                query = query.Where(c => dto.BrandIds.Contains(c.Model.Brand.Id));

            if (dto.ModelIds?.Any() == true)
                query = query.Where(c => dto.ModelIds.Contains(c.Model.Id));

            if (dto.DeliveryTypeIds?.Any() == true)
                query = query.Where(c => dto.DeliveryTypeIds.Contains(c.DeliveryTypeId));

            if (dto.SeatCounts?.Any() == true)
                query = query.Where(c => dto.SeatCounts.Contains(c.SeatCount));

            // Range-lər
            if (dto.LimitRangeId.HasValue)
            {
                var limitRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.LimitRangeId.Value);
                if (limitRange != null)
                {
                    query = query.Where(c => c.Limit >= limitRange.MinValue && c.Limit <= limitRange.MaxValue);
                }
            }
            if (dto.DailyPriceRangeId.HasValue)
            {
                var priceRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.DailyPriceRangeId.Value);
                if (priceRange != null)
                {
                    query = query.Where(c => c.DailyPrice >= priceRange.MinValue && c.DailyPrice <= priceRange.MaxValue);
                }
            }
            if (dto.DepositAmountRangeId.HasValue)
            {
                var depositRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.DepositAmountRangeId.Value);
                if (depositRange != null)
                {
                    query = query.Where(c => c.DepositAmount >= depositRange.MinValue && c.DepositAmount <= depositRange.MaxValue);
                }
            }

            var fuelCounts = await query
                .GroupBy(c => c.FuelId)
                .Select(g => new FilterCountDto
                {
                    Id = g.Key,
                    Name = g.First().Fuel.Name,
                    Count = g.Count()
                })
                .ToListAsync();

            return fuelCounts;
        }
        private async Task<List<FilterCountDto>> CalculateModelCountsAsync(CarSearchListDto dto, IQueryable<Car> query)
        {


            // Bütün filtrlər, AMMA ModelIds yox
            if (dto.ClassIds?.Any() == true)
                query = query.Where(c => dto.ClassIds.Contains(c.ClassId));
            if (dto.GearIds?.Any() == true)
                query = query.Where(c => dto.GearIds.Contains(c.GearId));
            if (dto.CompanyIds?.Any() == true)
                query = query.Where(c => dto.CompanyIds.Contains(c.CompanyId));
            if (dto.FuelIds?.Any() == true)
                query = query.Where(c => dto.FuelIds.Contains(c.FuelId));
            if (dto.BrandIds?.Any() == true)
                query = query.Where(c => dto.BrandIds.Contains(c.Model.Brand.Id));
            if (dto.DeliveryTypeIds?.Any() == true)
                query = query.Where(c => dto.DeliveryTypeIds.Contains(c.DeliveryTypeId));
            if (dto.SeatCounts?.Any() == true)
                query = query.Where(c => dto.SeatCounts.Contains(c.SeatCount));

            // Range-lər
            if (dto.LimitRangeId.HasValue)
            {
                var limitRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.LimitRangeId.Value);
                if (limitRange != null)
                {
                    query = query.Where(c => c.Limit >= limitRange.MinValue && c.Limit <= limitRange.MaxValue);
                }
            }
            if (dto.DailyPriceRangeId.HasValue)
            {
                var priceRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.DailyPriceRangeId.Value);
                if (priceRange != null)
                {
                    query = query.Where(c => c.DailyPrice >= priceRange.MinValue && c.DailyPrice <= priceRange.MaxValue);
                }
            }
            if (dto.DepositAmountRangeId.HasValue)
            {
                var depositRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.DepositAmountRangeId.Value);
                if (depositRange != null)
                {
                    query = query.Where(c => c.DepositAmount >= depositRange.MinValue && c.DepositAmount <= depositRange.MaxValue);
                }
            }


            var modelCounts = await query
                .GroupBy(c => c.Model.Id)
                .Select(g => new FilterCountDto
                {
                    Id = g.Key,
                    Name = g.First().Model.Name,
                    Count = g.Count()
                })
                .ToListAsync();

            return modelCounts;
        }
        private async Task<List<FilterCountDto>> CalculateDeliveryTypeCountsAsync(CarSearchListDto dto, IQueryable<Car> query)
        {

            // Bütün filtrlər, AMMA DeliveryTypeIds yox
            if (dto.ClassIds?.Any() == true)
                query = query.Where(c => dto.ClassIds.Contains(c.ClassId));
            if (dto.GearIds?.Any() == true)
                query = query.Where(c => dto.GearIds.Contains(c.GearId));
            if (dto.CompanyIds?.Any() == true)
                query = query.Where(c => dto.CompanyIds.Contains(c.CompanyId));
            if (dto.FuelIds?.Any() == true)
                query = query.Where(c => dto.FuelIds.Contains(c.FuelId));
            if (dto.BrandIds?.Any() == true)
                query = query.Where(c => dto.BrandIds.Contains(c.Model.Brand.Id));
            if (dto.ModelIds?.Any() == true)
                query = query.Where(c => dto.ModelIds.Contains(c.Model.Id));
            if (dto.SeatCounts?.Any() == true)
                query = query.Where(c => dto.SeatCounts.Contains(c.SeatCount));

            // Range-lər
            if (dto.LimitRangeId.HasValue)
            {
                var limitRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.LimitRangeId.Value);
                if (limitRange != null)
                {
                    query = query.Where(c => c.Limit >= limitRange.MinValue && c.Limit <= limitRange.MaxValue);
                }
            }
            if (dto.DailyPriceRangeId.HasValue)
            {
                var priceRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.DailyPriceRangeId.Value);
                if (priceRange != null)
                {
                    query = query.Where(c => c.DailyPrice >= priceRange.MinValue && c.DailyPrice <= priceRange.MaxValue);
                }
            }
            if (dto.DepositAmountRangeId.HasValue)
            {
                var depositRange = await _unitOfWork.FilterRangeRepository.GetEntity(r => r.Id == dto.DepositAmountRangeId.Value);
                if (depositRange != null)
                {
                    query = query.Where(c => c.DepositAmount >= depositRange.MinValue && c.DepositAmount <= depositRange.MaxValue);
                }
            }


            var deliveryTypeCounts = await query
                .GroupBy(c => c.DeliveryTypeId)
                .Select(g => new FilterCountDto
                {
                    Id = g.Key,
                    Name = g.First().DeliveryType.Name,
                    Count = g.Count()
                })
                .ToListAsync();

            return deliveryTypeCounts;
        }




















        //public async Task<List<CarListItemDto>> GetAllFilteredListCarsAsync(CarSearchListDto carSearchListDto)
        //{
        //    var cacheKey = GenerateCacheKey(carSearchListDto);

        //    var cachedCars = await _fusionCache.GetOrDefaultAsync<List<CarListItemDto>>(cacheKey);
        //    if (cachedCars is not null)
        //        return cachedCars;

        //    // `GetQuery` ilə əsas sorğunu alın
        //    var query = await _unitOfWork.CarRepository.GetQuery(
        //        includes: q => q
        //            .Include(c => c.Model)
        //                .ThenInclude(m => m.Brand)
        //            .Include(c => c.Model)
        //                .ThenInclude(m => m.CarPhoto)
        //            .Include(c => c.Body)
        //            .Include(c => c.Class)
        //            .Include(c => c.Fuel)
        //            .Include(c => c.Gear)
        //            .Include(c => c.Location)
        //            .Include(c => c.Company)
        //            .Include(c => c.DeliveryType)
        //    );

        //    // Aktiv və rezerv olunmayan maşınları filtrlə
        //    query = query.Where(c => c.IsActive && !c.IsReserved);

        //    // BrandIds filtr
        //    if (carSearchListDto.BrandIds != null && carSearchListDto.BrandIds.Any())
        //    {
        //        query = query.Where(c => carSearchListDto.BrandIds.Contains(c.Model.Brand.Id));
        //    }

        //    // ModelIds filtr
        //    if (carSearchListDto.ModelIds != null && carSearchListDto.ModelIds.Any())
        //    {
        //        query = query.Where(c => carSearchListDto.ModelIds.Contains(c.Model.Id));
        //    }

        //    // GearIds filtr
        //    if (carSearchListDto.GearIds != null && carSearchListDto.GearIds.Any())
        //    {
        //        query = query.Where(c => carSearchListDto.GearIds.Contains(c.GearId));
        //    }

        //    // CompanyIds filtr
        //    if (carSearchListDto.CompanyIds != null && carSearchListDto.CompanyIds.Any())
        //    {
        //        query = query.Where(c => carSearchListDto.CompanyIds.Contains(c.CompanyId));
        //    }

        //    // FuelIds filtr
        //    if (carSearchListDto.FuelIds != null && carSearchListDto.FuelIds.Any())
        //    {
        //        query = query.Where(c => carSearchListDto.FuelIds.Contains(c.FuelId));
        //    }

        //    // DeliveryTypeIds filtr
        //    if (carSearchListDto.DeliveryTypeIds != null && carSearchListDto.DeliveryTypeIds.Any())
        //    {
        //        query = query.Where(c => carSearchListDto.DeliveryTypeIds.Contains(c.DeliveryTypeId));
        //    }

        //    // SeatCounts filtr
        //    if (carSearchListDto.SeatCounts != null && carSearchListDto.SeatCounts.Any())
        //    {
        //        query = query.Where(c => carSearchListDto.SeatCounts.Contains(c.SeatCount));
        //    }

        //    // Limits filtr
        //    if (carSearchListDto.LimitRange.MinLimit.HasValue && carSearchListDto.LimitRange.MaxLimit.HasValue)
        //    {
        //        query = query.Where(c => c.Limit >= carSearchListDto.LimitRange.MinLimit.Value &&
        //                                 c.Limit <= carSearchListDto.LimitRange.MaxLimit.Value);
        //    }
        //    // DailyPrice filtr
        //    if (carSearchListDto.DailyPriceRange.MinPrice.HasValue && carSearchListDto.DailyPriceRange.MaxPrice.HasValue)
        //    {
        //        query = query.Where(c => c.DailyPrice >= carSearchListDto.DailyPriceRange.MinPrice.Value &&
        //                                 c.DailyPrice <= carSearchListDto.DailyPriceRange.MaxPrice.Value);
        //    }

        //    // DepositAmount filtr
        //    if (carSearchListDto.DepositAmountRange.MinAmount.HasValue && carSearchListDto.DepositAmountRange.MaxAmount.HasValue)
        //    {
        //        query = query.Where(c => c.DepositAmount >= carSearchListDto.DepositAmountRange.MinAmount.Value &&
        //                                 c.DepositAmount <= carSearchListDto.DepositAmountRange.MaxAmount.Value);
        //    }

        //    var cars = await query.ToListAsync();

        //    var mappedCars = _mapper.Map<List<CarListItemDto>>(cars);

        //    await _fusionCache.SetAsync(cacheKey, mappedCars, TimeSpan.FromMinutes(2));

        //    return mappedCars;
        //}

        // Helper method to generate a unique cache key based on filters
        private string GenerateCacheKey(CarSearchListDto filters)
        {
            return $"cars-{string.Join("-", filters.BrandIds ?? new List<int>())}" +
                   $"-{string.Join("-", filters.ModelIds ?? new List<int>())}" +
                   $"-{string.Join("-", filters.GearIds ?? new List<int>())}" +
                   $"-{string.Join("-", filters.CompanyIds ?? new List<int>())}" +
                   $"-{string.Join("-", filters.FuelIds ?? new List<int>())}" +
                   $"-{string.Join("-", filters.DeliveryTypeIds ?? new List<int>())}" +
                   $"-{string.Join("-", filters.SeatCounts ?? new List<int>())}";
        }
    }
}
