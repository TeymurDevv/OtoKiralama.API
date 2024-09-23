using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using OtoKiralama.Application.Dtos.Gear;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Persistance.Data.Implementations;

namespace OtoKiralama.Application.Services
{
    [Authorize(Roles = "admin")]
    public class GearService : IGearService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GearService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task CreateGearAsync(GearCreateDto gearCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGearAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GearReturnDto>> GetAllGearsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GearReturnDto> GetGearByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
