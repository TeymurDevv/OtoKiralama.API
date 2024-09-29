using AutoMapper;
using OtoKiralama.Application.Dtos.Body;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Persistance.Data.Implementations;

namespace OtoKiralama.Application.Services
{
    public class BodyService : IBodyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BodyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task CreateGearAsync(BodyCreateDto bodyCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGearAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BodyReturnDto>> GetAllGearsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BodyReturnDto> GetGearByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
