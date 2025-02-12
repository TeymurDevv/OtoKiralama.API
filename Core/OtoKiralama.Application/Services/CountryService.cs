using AutoMapper;
using OtoKiralama.Application.Dtos.Country;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateCountryAsync(CountryCreateDto countryCreateDto)
        {
            var existBody = await _unitOfWork.CountryRepository.isExists(c => c.Name.ToLower() == countryCreateDto.Name.ToLower());

            if (existBody)
                throw new CustomException(400, "Name", "Country already exist with this name");

            var country = _mapper.Map<Country>(countryCreateDto);
            await _unitOfWork.CountryRepository.Create(country);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResponse<CountryListItemDto>> GetAllCountriesAsync(int pageNumber, int pageSize)
        {
            int totalCountries = await _unitOfWork.CountryRepository.CountAsync();
            var countries = await _unitOfWork.CountryRepository.GetAll(
                includes: query => query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                );
            return new PagedResponse<CountryListItemDto>
            {
                Data = _mapper.Map<List<CountryListItemDto>>(countries),
                TotalCount = 10,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }
    }
}
