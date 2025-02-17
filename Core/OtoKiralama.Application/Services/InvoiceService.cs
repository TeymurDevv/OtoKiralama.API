using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.IndividualInvoice;
using OtoKiralama.Application.Dtos.Invoice;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Enums;
using OtoKiralama.Domain.Repositories;



namespace OtoKiralama.Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserResolverService _userResolverService;


    public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserResolverService userResolverService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _userResolverService = userResolverService;
    }

    public async Task<InvoiceReturnDto> GetInvoiceByUserIdAsync()
    {
        var userId = await _userResolverService.GetCurrentUserIdAsync();

        var existUser = await _userManager.Users.Include(u => u.Invoice).ThenInclude(i => i.Country).FirstOrDefaultAsync(u => u.Id == userId);
        if (existUser == null)
            throw new CustomException(404, "UserId", "User not found");

        if (existUser.Invoice == null)
            throw new CustomException(404, "Invoice", "Invoice not found");
        return _mapper.Map<InvoiceReturnDto>(existUser.Invoice);
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
    public async Task<InvoiceReturnDto> CreateInvoiceAsync(InvoiceCreateDto dto)
    {
        var userId = await _userResolverService.GetCurrentUserIdAsync();
        var existUser = await _userManager.Users.Include(u => u.Invoice).FirstOrDefaultAsync(u => u.Id == userId);
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
        existUser.Invoice = invoice;
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<InvoiceReturnDto>(existUser.Invoice);
    }

    public async Task UpdateInvoiceAsync(InvoiceUpdateDto dto)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var userId = await _userResolverService.GetCurrentUserIdAsync();
            var existUser = await _userManager.Users
                .Include(u => u.Invoice)
                .ThenInclude(i => i.Country)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (existUser == null)
                throw new CustomException(404, "UserId", "User not found");

            if (existUser.Invoice == null)
                throw new CustomException(404, "Invoice", "Invoice not found");

            var existCountry = await _unitOfWork.CountryRepository.GetEntity(c => c.Id == dto.CountryId);
            if (existCountry == null)
                throw new CustomException(404, "CountryId", "Country not found by this id");

            if (existUser.Invoice.InvoiceType != dto.InvoiceType)
            {
                Invoice newInvoice = dto.InvoiceType switch
                {
                    InvoiceType.IndividualInvoice => _mapper.Map<IndividualInvoice>(dto),
                    InvoiceType.IndividualCompanyInvoice => _mapper.Map<IndividualCompanyInvoice>(dto),
                    InvoiceType.CompanyInvoice => _mapper.Map<CorporateInvoice>(dto),
                    _ => throw new ArgumentException("Yanlış invoice növü.")
                };

                newInvoice.AppUserId = userId;

                switch (existUser.Invoice)
                {
                    case IndividualInvoice individual:
                        await _unitOfWork.IndividualInvoiceRepository.Delete(individual);
                        break;
                    case IndividualCompanyInvoice individualCompany:
                        await _unitOfWork.IndividualCompanyInvoiceRepository.Delete(individualCompany);
                        break;
                    case CorporateInvoice corporate:
                        await _unitOfWork.CorporateInvoiceRepository.Delete(corporate);
                        break;
                    default:
                        throw new ArgumentException("Yanlış invoice tipi.");
                }

                switch (newInvoice)
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
            }
            else
            {
                switch (existUser.Invoice)
                {
                    case IndividualInvoice individual:
                        _mapper.Map(dto, individual);
                        break;
                    case IndividualCompanyInvoice individualCompany:
                        _mapper.Map(dto, individualCompany);
                        break;
                    case CorporateInvoice corporate:
                        _mapper.Map(dto, corporate);
                        break;
                }
            }

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }








}