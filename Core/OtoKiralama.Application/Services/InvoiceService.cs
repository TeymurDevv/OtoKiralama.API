using AutoMapper;
using OtoKiralama.Application.Dtos.IndividualInvoice;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<IndividualInvoice>> GetAllInvoicesAsync()
    {
        var data = await _unitOfWork.IndividualInvoiceRepository.GetAll();
        return data;
    }

    public async Task CreateInvoiceAsync(IndividualInvoiceCreateDto individualInvoiceCreateDto)
    {
        var individualInvoice = _mapper.Map<IndividualInvoice>(individualInvoiceCreateDto);
        await _unitOfWork.IndividualInvoiceRepository.Create(individualInvoice);
        await _unitOfWork.SaveChangesAsync();
    }
}