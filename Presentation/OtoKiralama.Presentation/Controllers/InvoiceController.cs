using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.IndividualInvoice;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;
    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }
    [HttpPost("")]
    public async Task<IActionResult> CreateInvoiceAsync(InvoiceCreateDto invoiceCreateDto)
    {
        await _invoiceService.CreateInvoiceAsync(invoiceCreateDto);
        return StatusCode(StatusCodes.Status201Created);
    }
    [HttpGet("")]
    public async Task<IActionResult> GetAllInvoices()
    {
        return Ok(await _invoiceService.GetAllInvoicesAsync());
    }
}