using OtoKiralama.Application.Dtos.Country;
using OtoKiralama.Application.Dtos.Pagination;

namespace OtoKiralama.Application.Interfaces
{
    public interface ICountryService
    {
        Task CreateCountryAsync(CountryCreateDto countryCreateDto);
        Task<PagedResponse<CountryListItemDto>> GetAllCountriesAsync(int pageNumber, int pageSize);
        Task UpdateCountryAsync(int id, CountryUpdateDto countryUpdateDto);
        Task DeleteCountryAsync(int id);
        Task<CountryReturnDto> GetCountryByIdAsync(int id);

    }
}
