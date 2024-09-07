using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Data;

namespace OtoKiralama.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BrandService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateBrandAsync(BrandCreateDto brandCreateDto)
        {
            var brand = _mapper.Map<Brand>(brandCreateDto);
            var existBrand = await _context.Brands.AnyAsync(c => c.Name == brand.Name);
            if (existBrand)
                throw new CustomException(400, "Name", "Brand already exist with this name");
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBrandAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand is null)
                throw new CustomException(404, "Id", "Brand not found with this Id");
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BrandReturnDto>> GetAllBrandsAsync()
        {
            return await _context.Brands.Select(b => _mapper.Map<BrandReturnDto>(b)).ToListAsync();
        }

        public async Task<BrandReturnDto> GetBrandByIdAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand is null)
                throw new CustomException(404, "Id", "Brand not found with this Id");
            return _mapper.Map<BrandReturnDto>(brand);
        }
    }
}
