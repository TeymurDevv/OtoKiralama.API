using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Application.Dtos.Location;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Data;

namespace OtoKiralama.Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LocationService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateLocationAsync(LocationCreateDto locationCreateDto)
        {
            var location = _mapper.Map<Location>(locationCreateDto);
            var existLocation = await _context.Locations.AnyAsync(b => b.Name == location.Name);
            if (existLocation)
                throw new CustomException(400, "Name", "Location already exist with this name");
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteLocationAsync(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location is null)
                throw new CustomException(404, "Id", "Location not found with this Id");
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
        }
        public async Task<List<LocationReturnDto>> GetAllLocationsAsync()
        {
            return await _context.Locations.Select(b => _mapper.Map<LocationReturnDto>(b)).ToListAsync();
        }
        public async Task<LocationReturnDto> GetLocationByIdAsync(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location is null)
                throw new CustomException(404, "Id", "Location not found with this Id");
            return _mapper.Map<LocationReturnDto>(location);
        }
    }
}
