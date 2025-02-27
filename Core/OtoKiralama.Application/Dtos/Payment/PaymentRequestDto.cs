namespace OtoKiralama.Application.Dtos.Payment;

public class PaymentRequestDto
{
    public int ReservationId { get; set; }
    public string PaymentMethodId { get; set; } // Stripe Payment Method ID (from frontend)
}