using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.IndividualInvoice;
using OtoKiralama.Application.Dtos.Invoice;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Enums;
using OtoKiralama.Domain.Repositories;
using System.Security.Claims;



namespace OtoKiralama.Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<List<InvoiceReturnDto>> GetAllInvoicesAsync()
    {
        var individualInvoices = await _unitOfWork.IndividualInvoiceRepository.GetAll();
        var individualCompanyInvoices = await _unitOfWork.IndividualCompanyInvoiceRepository.GetAll();
        var corporateInvoices = await _unitOfWork.CorporateInvoiceRepository.GetAll();

        var allInvoices = individualInvoices.Cast<Invoice>()
            .Concat(individualCompanyInvoices)
            .Concat(corporateInvoices)
            .ToList();

        return _mapper.Map<List<InvoiceReturnDto>>(allInvoices);
    }


    public async Task CreateInvoiceAsync(InvoiceCreateDto dto)
    {
        var existUser = await _userManager.Users.Include(u => u.Invoice).FirstOrDefaultAsync(u => u.Id == dto.AppUserId);
        if (existUser == null)
            throw new CustomException(404, "UserId", "User not found");

        if (existUser.Invoice != null)
            throw new CustomException(404, "UserId", "This User already has any invoice");


        var existCountry = await _unitOfWork.CountryRepository.GetEntity(c => c.Id == dto.CountryId);
        if (existCountry == null)
            throw new CustomException(404, "CountryId", "Country not found by this id");


        Invoice invoice = dto.InvoiceType switch
        {
            InvoiceType.IndividualInvoice => _mapper.Map<IndividualInvoice>(dto),
            InvoiceType.IndividualCompanyInvoice => _mapper.Map<IndividualCompanyInvoice>(dto),
            InvoiceType.CompanyInvoice => _mapper.Map<CorporateInvoice>(dto),
            _ => throw new ArgumentException("Yanlış invoice növü.")
        };

        switch (invoice)
        {
            case IndividualInvoice individual:
                await _unitOfWork.IndividualInvoiceRepository.Create(individual);
                break;
            case IndividualCompanyInvoice individualCompany:
                await _unitOfWork.IndividualCompanyInvoiceRepository.Create(individualCompany);
                break;
            case CorporateInvoice corporate:
                await _unitOfWork.CorporateInvoiceRepository.Create(corporate);
                break;
        }

        await _unitOfWork.SaveChangesAsync();
    }
    public async Task DeleteInvoice(int id,InvoiceType invoiceType)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new CustomException(403,"UserId", "User ID cannot be null");
        }
        var existedUser=await _userManager.FindByIdAsync(userId);
        if (existedUser is null)
            throw new CustomException(404,"User", "User  not found");
        switch (invoiceType)
        {
            case InvoiceType.IndividualInvoice:
               var existedIndividualInvoice= await _unitOfWork.IndividualInvoiceRepository.GetEntity(s=>s.Id== id&&s.AppUserId==userId);
                if(existedIndividualInvoice is null)
                    throw new CustomException(404, "Invoice", "Invoice  not found");
                await _unitOfWork.IndividualInvoiceRepository.Delete(existedIndividualInvoice);
               await _unitOfWork.SaveChangesAsync();
                break;
                case InvoiceType.IndividualCompanyInvoice:
                var existedIndividualCompanyInvoice = await _unitOfWork.IndividualCompanyInvoiceRepository.GetEntity(s => s.Id == id && s.AppUserId == userId);
                if (existedIndividualCompanyInvoice is null)
                    throw new CustomException(404, "Invoice", "Invoice  not found");
                await _unitOfWork.IndividualCompanyInvoiceRepository.Delete(existedIndividualCompanyInvoice);
                await _unitOfWork.SaveChangesAsync();
                break;
                case InvoiceType.CompanyInvoice:
                var existedCompanyInvoice = await _unitOfWork.CorporateInvoiceRepository.GetEntity(s => s.Id == id && s.AppUserId == userId);
                if (existedCompanyInvoice is null)
                    throw new CustomException(404, "Invoice", "Invoice  not found");
                await _unitOfWork.CorporateInvoiceRepository.Delete(existedCompanyInvoice);
                await _unitOfWork.SaveChangesAsync();
                break;
            default:
                throw new ArgumentException("wrong one");


        }
    }



}