﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OtoKiralama.Application.Dtos.Company;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;
using System.Data.Entity;

namespace OtoKiralama.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserResolverService _userResolverService;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService, IUserResolverService userResolverService, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoService = photoService;
            _userResolverService = userResolverService;
            _userManager = userManager;
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
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCompanyAsync(int id, CompanyUpdateDto companyUpdateDto)
        {
            var existUserId = await _userResolverService.GetCurrentUserIdAsync();
            if (existUserId is null)
                throw new CustomException(404, "UserId", "User not found");

            var existUser = await _userManager.FindByIdAsync(existUserId);
            if (existUser == null)
                throw new CustomException(404, "UserId", "User not found");

            if (existUser.CompanyId != id)
                throw new CustomException(404, "Id", "You have not access update this Company");
            var existCompany = await _unitOfWork.CompanyRepository.GetEntity(c => c.Id == id);
            if (existCompany == null) throw new CustomException(404, "Id", "Company not found");

            _mapper.Map(companyUpdateDto, existCompany);
            _unitOfWork.CompanyRepository.Update(existCompany);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCompanyFullAsync(int id, CompanyFullUpdateDto companyFullUpdateDto)
        {
            var existCompany = await _unitOfWork.CompanyRepository.GetEntity(c => c.Id == id);
            if (existCompany == null) throw new CustomException(404, "Id", "Company not found");
            var isExistCompany = await _unitOfWork.CompanyRepository.isExists(c => c.Name == companyFullUpdateDto.Name && c.Id != id);
            if (isExistCompany)
                throw new CustomException(400, "Name", "Company already exist with this name");

            _mapper.Map(companyFullUpdateDto, existCompany);
            if (companyFullUpdateDto.ImageFile != null)
            {
                try
                {
                    string newImageUrl = await _photoService.UploadPhotoAsync(companyFullUpdateDto.ImageFile);

                    if (!string.IsNullOrEmpty(existCompany.ImageUrl))
                    {
                        await _photoService.DeletePhotoAsync(existCompany.ImageUrl);
                    }
                    existCompany.ImageUrl = newImageUrl;

                }
                catch (Exception ex)
                {
                    throw new CustomException(500, "ImageUpload", "Failed to upload the new image");
                }
            }
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteCompanyAsync(int id)
        {
            var company = await _unitOfWork.CompanyRepository.GetEntity(c => c.Id == id);
            if (company is null)
                throw new CustomException(404, "Id", "Company not found with this Id");

            await _photoService.DeletePhotoAsync(company.ImageUrl);
            await _unitOfWork.CompanyRepository.Delete(company);
            await _unitOfWork.SaveChangesAsync();
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
