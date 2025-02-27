using FluentValidation;
using OtoKiralama.Application.Dtos.Payment;

namespace OtoKiralama.Application.Validators.PaymentValidator;

public class PaymentRequestValidator : AbstractValidator<PaymentRequestDto>
{
    public PaymentRequestValidator()
    {
        RuleFor(p => p.ReservationId).NotEmpty()
            .WithMessage("Odeme esnasinda sorun cikti");
        RuleFor(p => p.PaymentMethodId).NotEmpty()
            .WithMessage("Odeme esnasinda sorun cikti");
    }
}