using AutoMapper;
using OtoKiralama.Application.Dtos.Location;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Data;
using OtoKiralama.Persistance.Data.Implementations;
using ZiggyCreatures.Caching.Fusion;

namespace OtoKiralama.Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFusionCache _fusionCache;

        public LocationService(AppDbContext context, IMapper mapper, IUnitOfWork unitOfWork, IFusionCache fusionCache)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fusionCache = fusionCache;
        }

        public async Task CreateLocationAsync(LocationCreateDto locationCreateDto)
        {
            var location = _mapper.Map<Location>(locationCreateDto);
            var existLocation = await _unitOfWork.LocationRepository.isExists(l => l.Name == location.Name);
            if (existLocation)
                throw new CustomException(400, "Name", "Location already exist with this name");
            await _unitOfWork.LocationRepository.Create(location);
            await _unitOfWork.CommitAsync();
        }
        public async Task DeleteLocationAsync(int id)
        {
            var location = await _unitOfWork.LocationRepository.GetEntity(l => l.Id == id);
            if (location is null)
                throw new CustomException(404, "Id", "Location not found with this Id");
            await _unitOfWork.LocationRepository.Delete(location);
            await _unitOfWork.CommitAsync();
        }
        public async Task<PagedResponse<LocationListItemDto>> GetAllLocationsAsync(int pageNumber, int pageSize)
        {
            int totalLocations = await _unitOfWork.LocationRepository.CountAsync();
            var locations = await _unitOfWork.LocationRepository.GetAll(
                includes: query => query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                );
            return new PagedResponse<LocationListItemDto>
            {
                Data = _mapper.Map<List<LocationListItemDto>>(locations),
                TotalCount = totalLocations,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<PagedResponse<LocationListItemDto>> GetAllLocationsByNameAsync(
            string name, int pageNumber, int pageSize)
        {
            var cacheKey = $"locations-{name}-{pageNumber}-{pageSize}";

            var cachedLocations = await _fusionCache.GetOrDefaultAsync<PagedResponse<LocationListItemDto>>(cacheKey);
            if (cachedLocations is not null)
                return cachedLocations;

            var locations = await _unitOfWork.LocationRepository.GetAll(
                includes: query => query
                    .Where(l => l.Name.Contains(name))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
            );

            int totalCount = await _unitOfWork.LocationRepository.CountAsync(l=>l.Name.Contains(name));

            var pagedResponse = new PagedResponse<LocationListItemDto>
            {
                Data = _mapper.Map<List<LocationListItemDto>>(locations),
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };

            await _fusionCache.SetAsync(cacheKey, pagedResponse, TimeSpan.FromMinutes(2));

            return pagedResponse;
        }

        public async Task<LocationReturnDto> GetLocationByIdAsync(int id)
        {
            var location = await _unitOfWork.LocationRepository.GetEntity(l => l.Id == id);
            if (location is null)
                throw new CustomException(404, "Id", "Location not found with this Id");
            return _mapper.Map<LocationReturnDto>(location);
        }
    }
}
