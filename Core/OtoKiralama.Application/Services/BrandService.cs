using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.Brand;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;
namespace OtoKiralama.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateBrandAsync(BrandCreateDto brandCreateDto)
        {
            var existBrand = await _unitOfWork.BrandRepository.isExists(b => b.Name.ToLower() == brandCreateDto.Name.ToLower());
            if (existBrand)
                throw new CustomException(400, "Name", "Brand already exist with this name");

            var brand = _mapper.Map<Brand>(brandCreateDto);

            await _unitOfWork.BrandRepository.Create(brand);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task UpdateBrandAsync(int id, BrandUpdateDto brandUpdateDto)
        {
            var brand = await _unitOfWork.BrandRepository.GetEntity(b => b.Id == id);
            if (brand is null)
                throw new CustomException(404, "Id", "Brand not found with this Id");
            var existBrand = await _unitOfWork.BrandRepository.isExists(b => b.Name.ToLower() == brandUpdateDto.Name.ToLower() && b.Id != id);
            if (existBrand)
                throw new CustomException(400, "Name", "Another brand already exists with this name");


            _mapper.Map(brandUpdateDto, brand);
            _unitOfWork.BrandRepository.Update(brand);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteBrandAsync(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var brand = await _unitOfWork.BrandRepository.GetEntity(
                    b => b.Id == id,
                    includes: query => query
                        .Include(b => b.Models)
                        .ThenInclude(m => m.Cars)
                );

                if (brand is null)
                    throw new CustomException(404, "Id", "Brand not found with this Id");

                var carsToDelete = brand.Models.SelectMany(m => m.Cars).ToList();
                if (carsToDelete.Count > 0)
                    await _unitOfWork.CarRepository.DeleteRangeAsync(carsToDelete);

                var modelsToDelete = brand.Models.ToList();
                if (modelsToDelete.Count > 0)
                    await _unitOfWork.ModelRepository.DeleteRangeAsync(modelsToDelete);

                await _unitOfWork.BrandRepository.Delete(brand);

                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }



        public async Task<PagedResponse<BrandListItemDto>> GetAllBrandsAsync(int pageNumber, int pageSize)
        {
            int totalBrands = await _unitOfWork.BrandRepository.CountAsync();
            var brands = await _unitOfWork.BrandRepository.GetAll(
                includes: query => query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                );
            return new PagedResponse<BrandListItemDto>
            {
                Data = _mapper.Map<List<BrandListItemDto>>(brands),
                TotalCount = totalBrands,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
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
