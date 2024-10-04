using AutoMapper;
using OtoKiralama.Application.Dtos.Body;
using OtoKiralama.Application.Dtos.Company;
using OtoKiralama.Application.Dtos.Fuel;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Data.Implementations;

namespace OtoKiralama.Application.Services
{
    public class FuelService : IFuelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FuelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateFuelAsync(FuelCreateDto fuelCreateDto)
        {
            var fuel = _mapper.Map<Fuel>(fuelCreateDto);
            var existFuel = await _unitOfWork.FuelRepository.isExists(f => f.Name == fuelCreateDto.Name);
            if (existFuel)
                throw new CustomException(400, "Name", "Fuel already exist with this name");
            await _unitOfWork.FuelRepository.Create(fuel);
            _unitOfWork.Commit();
        }

        public async Task DeleteFuelAsync(int id)
        {
            var fuel = await _unitOfWork.FuelRepository.GetEntity(f => f.Id == id);
            if (fuel is null)
                throw new CustomException(404, "Id", "Fuel not found with this Id");
            await _unitOfWork.FuelRepository.Delete(fuel);
            _unitOfWork.Commit();
        }

        public async Task<PagedResponse<FuelListItemDto>> GetAllFuelsAsync(int pageNumber, int pageSize)
        {
            int totalFuels = await _unitOfWork.FuelRepository.CountAsync();
            var fuels = await _unitOfWork.FuelRepository.GetAll(
                includes: query => query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                );
            return new PagedResponse<FuelListItemDto>
            {
                Data = _mapper.Map<List<FuelListItemDto>>(fuels),
                TotalCount = totalFuels,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<FuelReturnDto> GetFuelByIdAsync(int id)
        {
            var fuel = await _unitOfWork.FuelRepository.GetEntity(f => f.Id == id);
            if (fuel is null)
                throw new CustomException(404, "Id", "Fuel not found with this Id");
            return _mapper.Map<FuelReturnDto>(fuel);
        }
    }
}
