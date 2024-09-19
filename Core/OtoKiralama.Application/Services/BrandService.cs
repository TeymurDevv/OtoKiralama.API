using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Data;
using OtoKiralama.Persistance.Data.Implementations;

namespace OtoKiralama.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandService(AppDbContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateBrandAsync(BrandCreateDto brandCreateDto)
        {
            var brand = _mapper.Map<Brand>(brandCreateDto);
            var existBrand = await _unitOfWork.BrandRepository.isExists(b => b.Name == brand.Name);
            if (existBrand)
                throw new CustomException(400, "Name", "Brand already exist with this name");
            await _unitOfWork.BrandRepository.Create(brand);
            _unitOfWork.Commit();
        }

        public async Task DeleteBrandAsync(int id)
        {
            var brand = await _unitOfWork.BrandRepository.GetEntity(b => b.Id == id);
            if (brand is null)
                throw new CustomException(404, "Id", "Brand not found with this Id");
            await _unitOfWork.BrandRepository.Delete(brand);
            _unitOfWork.Commit();
        }

        public async Task<List<BrandReturnDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.BrandRepository.GetAll();
            return _mapper.Map<List<BrandReturnDto>>(brands);
        }

        public async Task<BrandReturnDto> GetBrandByIdAsync(int id)
        {
            var brand = await _unitOfWork.BrandRepository.GetEntity(b => b.Id == id);
            if (brand is null)
                throw new CustomException(404, "Id", "Brand not found with this Id");
            return _mapper.Map<BrandReturnDto>(brand);
        }
    }
}
