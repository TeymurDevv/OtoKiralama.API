using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Application.Dtos.Location;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Data;
using OtoKiralama.Persistance.Data.Implementations;

namespace OtoKiralama.Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LocationService(AppDbContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateLocationAsync(LocationCreateDto locationCreateDto)
        {
            var location = _mapper.Map<Location>(locationCreateDto);
            var existLocation = await _unitOfWork.LocationRepository.isExists(l => l.Name == location.Name);
            if (existLocation)
                throw new CustomException(400, "Name", "Location already exist with this name");
            await _unitOfWork.LocationRepository.Create(location);
            _unitOfWork.Commit();
        }
        public async Task DeleteLocationAsync(int id)
        {
            var location = await _unitOfWork.LocationRepository.GetEntity(l => l.Id == id);
            if (location is null)
                throw new CustomException(404, "Id", "Location not found with this Id");
            await _unitOfWork.LocationRepository.Delete(location);
            _unitOfWork.Commit();
        }
        public async Task<List<LocationReturnDto>> GetAllLocationsAsync()
        {
            var locations = await _unitOfWork.LocationRepository.GetAll();
            return _mapper.Map<List<LocationReturnDto>>(locations);
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
