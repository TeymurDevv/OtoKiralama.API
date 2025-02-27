using OtoKiralama.Application.Dtos.Payment;

namespace OtoKiralama.Application.Interfaces;

public interface IPaymentService
{
    public Task Pay(PaymentRequestDto paymentRequestDto);
}