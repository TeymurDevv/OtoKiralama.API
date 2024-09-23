using AutoMapper;
using OtoKiralama.Application.Dtos.Gear;
using OtoKiralama.Application.Dtos.Location;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Data.Implementations;

namespace OtoKiralama.Application.Services
{
    public class GearService : IGearService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GearService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateGearAsync(GearCreateDto gearCreateDto)
        {
            var gear = _mapper.Map<Location>(gearCreateDto);
            var existGear = await _unitOfWork.GearRepository.isExists(g => g.Name == g.Name);
            if (existGear)
                throw new CustomException(400, "Name", "Gear already exist with this name");
            await _unitOfWork.LocationRepository.Create(gear);
            _unitOfWork.Commit();
        }

        public async Task DeleteGearAsync(int id)
        {
            var gear = await _unitOfWork.GearRepository.GetEntity(g => g.Id == id);
            if (gear is null)
                throw new CustomException(404, "Id", "Gear not found with this Id");
            await _unitOfWork.GearRepository.Delete(gear);
            _unitOfWork.Commit();
        }

        public async Task<List<GearReturnDto>> GetAllGearsAsync()
        {
            var gears = await _unitOfWork.GearRepository.GetAll();
            return _mapper.Map<List<GearReturnDto>>(gears);
        }

        public async Task<GearReturnDto> GetGearByIdAsync(int id)
        {
            var gear = await _unitOfWork.GearRepository.GetEntity(g => g.Id == id);
            if (gear is null)
                throw new CustomException(404, "Id", "Gear not found with this Id");
            return _mapper.Map<GearReturnDto>(gear);
        }
    }
}
