using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Payment;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("Pay")]
    public IActionResult Pay(PaymentRequestDto paymentRequestDto)
    {
        _paymentService.Pay(paymentRequestDto);
        return Ok();
    }
}