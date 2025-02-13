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

        public async Task DeleteCountryAsync(int id)
        {
            var country = await _unitOfWork.CountryRepository.GetEntity(c => c.Id == id);
            if (country is null)
                throw new CustomException(404, "Id", "Country not found with this Id");
            await _unitOfWork.CountryRepository.Delete(country);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCountryAsync(int id, CountryUpdateDto countryUpdateDto)
        {
            var existCountry = await _unitOfWork.CountryRepository.GetEntity(c => c.Id == id);
            if (existCountry is null)
                throw new CustomException(404, "Id", "Country not found with this Id");

            var existCountryByName = await _unitOfWork.CountryRepository.isExists(c => c.Name.ToLower() == countryUpdateDto.Name.ToLower() && c.Id != id);
            if (existCountryByName)
                throw new CustomException(400, "Name", "Another Country already exists with this name");

            _mapper.Map(countryUpdateDto, existCountry);

            _unitOfWork.CountryRepository.Update(existCountry);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CountryReturnDto> GetCountryByIdAsync(int id)
        {
            var existCountry = await _unitOfWork.CountryRepository.GetEntity(c => c.Id == id);
            if (existCountry is null)
                throw new CustomException(404, "Id", "Country not found with this Id");
            return _mapper.Map<CountryReturnDto>(existCountry);
        }
    }
}
