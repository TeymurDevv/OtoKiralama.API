using AutoMapper;
using OtoKiralama.Application.Dtos.Fuel;
using OtoKiralama.Application.Interfaces;
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

        public Task CreateFuelAsync(FuelCreateDto fuelCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFuelAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FuelReturnDto>> GetAllFuelsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FuelReturnDto> GetFuelByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
