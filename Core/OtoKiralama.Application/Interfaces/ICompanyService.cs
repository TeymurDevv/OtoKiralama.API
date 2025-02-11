using OtoKiralama.Application.Dtos.Company;
using OtoKiralama.Application.Dtos.Pagination;

namespace OtoKiralama.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<PagedResponse<CompanyListItemDto>> GetAllCompaniesAsync(int pageNumber, int pageSize);
        Task<CompanyReturnDto> GetCompanyByIdAsync(int id);
        Task<CompanyReturnDto> GetCompanyByNameAsync(string name);
        Task CreateCompanyAsync(CompanyCreateDto companyCreateDto);
        //Task DeleteCompanyAsync(int id);
        Task UpdateCompanyAsync(int id, string userId, CompanyUpdateDto companyUpdateDto);
        Task UpdateCompanyFullAsync(int id, CompanyFullUpdateDto companyFullUpdateDto);
    }
}
