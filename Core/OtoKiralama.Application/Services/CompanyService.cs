using AutoMapper;
using OtoKiralama.Application.Dtos.Company;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Data.Implementations;

namespace OtoKiralama.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task<CompanyReturnDto> GetCompanyByNameAsync(string name)
        {
            var company = await _unitOfWork.CompanyRepository.GetEntity(c => c.Name == name);
            if (company == null)
                throw new CustomException(404, "Name", "Company not found with this Name");
            return _mapper.Map<CompanyReturnDto>(company);
        }

        public async Task CreateCompanyAsync(CompanyCreateDto companyCreateDto)
        {
            var existCompany = await _unitOfWork.CompanyRepository.isExists(c => c.Name == companyCreateDto.Name);
            if (existCompany)
                throw new CustomException(400, "Name", "Company already exist with this name");
            var company = _mapper.Map<Company>(companyCreateDto);
            string imageUrl = await _photoService.UploadPhotoAsync(companyCreateDto.ImageFile);
            company.ImageUrl = imageUrl;
            await _unitOfWork.CompanyRepository.Create(company);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCompanyAsync(int id)
        {
            var company = await _unitOfWork.CompanyRepository.GetEntity(c => c.Id == id);
            if (company is null)
                throw new CustomException(404, "Id", "Company not found with this Id");
            await _unitOfWork.CompanyRepository.Delete(company);
            await _photoService.DeletePhotoAsync(company.ImageUrl);
            await _unitOfWork.CommitAsync();
        }

        public async Task<PagedResponse<CompanyListItemDto>> GetAllCompaniesAsync(int pageNumber, int pageSize)
        {
            int totalCompanies = await _unitOfWork.CompanyRepository.CountAsync();
            var companies = await _unitOfWork.CompanyRepository.GetAll(
                includes: query => query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                );
            return new PagedResponse<CompanyListItemDto>
            {
                Data = _mapper.Map<List<CompanyListItemDto>>(companies),
                TotalCount = totalCompanies,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<CompanyReturnDto> GetCompanyByIdAsync(int id)
        {
            var company = await _unitOfWork.CompanyRepository.GetEntity(c => c.Id == id);
            if (company is null)
                throw new CustomException(404, "Id", "Company not found with this Id");
            return _mapper.Map<CompanyReturnDto>(company);
        }
    }
}
