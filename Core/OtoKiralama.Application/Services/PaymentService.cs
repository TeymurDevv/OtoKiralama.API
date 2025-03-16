using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OtoKiralama.Application.Dtos.Payment;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Application.Settings;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;
using Stripe;

namespace OtoKiralama.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUserResolverService _appUserResolver;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _stripeApiKey;
        private readonly IEmailService _emailService;

        public PaymentService(IUserResolverService appUserResolver, UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IOptions<StripeSettings> stripeSettings, IEmailService emailService)
        {
            _appUserResolver = appUserResolver;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _stripeApiKey = stripeSettings?.Value?.SecretKey;
        }

        public async Task Pay(PaymentRequestDto paymentRequestDto)
        {
            if (paymentRequestDto == null)
                throw new CustomException(400, "Payment", "Invalid payment request");

            Reservation existReservationWithThisUser = null;

            try
            {
                var existUserId = await _appUserResolver.GetCurrentUserIdAsync();
                if (string.IsNullOrWhiteSpace(existUserId))
                    throw new CustomException(400, "User", "User is not authenticated");

                var user = await _userManager.FindByIdAsync(existUserId);
                if (user == null)
                    throw new CustomException(400, "User", "User does not exist");

                existReservationWithThisUser = await _unitOfWork.ReservationRepository
                    .GetEntity(r => r.Id == paymentRequestDto.ReservationId && r.AppUserId == user.Id);

                if (existReservationWithThisUser == null)
                    throw new CustomException(400, "Reservation", "User does not have this kind of reservation");

                if (existReservationWithThisUser.IsPaid)
                    throw new CustomException(400, "Reservation", "Reservation is already paid");

                var paymentIntentOptions = new PaymentIntentCreateOptions
                {
                    Amount = (long)(existReservationWithThisUser.TotalPrice * 100),
                    Currency = "try",
                    PaymentMethodTypes = new List<string> { "card" },
                    PaymentMethod = paymentRequestDto.PaymentMethodId,
                    Confirm = true
                };

                var client = new StripeClient(_stripeApiKey);
                var paymentIntentService = new PaymentIntentService(client);
                var paymentIntent = await paymentIntentService.CreateAsync(paymentIntentOptions);

                if (paymentIntent.Status == "succeeded")
                {
                    existReservationWithThisUser.IsPaid = true;
                    await _unitOfWork.ReservationRepository.Update(existReservationWithThisUser);
                    await _unitOfWork.SaveChangesAsync();
                    string emailBody = @"
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <meta charset='UTF-8'>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                            <title>Fatura Onayı</title>
                            <style>
                                body { 
                                    font-family: 'Segoe UI', Arial, sans-serif; 
                                    background-color: #f8f9fa; 
                                    padding: 0;
                                    margin: 0;
                                    line-height: 1.6;
                                }
                                .container { 
                                    max-width: 600px; 
                                    margin: 0 auto; 
                                    background: #ffffff; 
                                    padding: 40px 20px;
                                    border-radius: 12px;
                                    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
                                }
                                .header {
                                    text-align: center;
                                    margin-bottom: 30px;
                                    padding-bottom: 20px;
                                    border-bottom: 2px solid #f0f0f0;
                                }
                                h2 { 
                                    color: #2c3e50; 
                                    text-align: center;
                                    font-size: 28px;
                                    margin: 0;
                                    font-weight: 600;
                                }
                                .greeting {
                                    color: #2c3e50;
                                    font-size: 16px;
                                    margin-bottom: 25px;
                                }
                                .invoice-details { 
                                    width: 100%; 
                                    border-collapse: separate;
                                    border-spacing: 0;
                                    margin: 25px 0;
                                    background: #f8f9fa;
                                    border-radius: 8px;
                                    overflow: hidden;
                                }
                                .invoice-details th, 
                                .invoice-details td { 
                                    padding: 15px;
                                    text-align: left;
                                    border-bottom: 1px solid #e9ecef;
                                }
                                .invoice-details th {
                                    background-color: #f8f9fa;
                                    color: #6c757d;
                                    font-weight: 600;
                                }
                                .invoice-details td {
                                    color: #2c3e50;
                                    font-weight: 500;
                                }
                                .total-section {
                                    background: #f8f9fa;
                                    padding: 20px;
                                    border-radius: 8px;
                                    margin: 25px 0;
                                    text-align: center;
                                }
                                .total {
                                    font-size: 24px;
                                    font-weight: 600;
                                    color: #2c3e50;
                                    margin: 0;
                                }

                                .footer { 
                                    text-align: center;
                                    font-size: 14px;
                                    color: #6c757d;
                                    margin-top: 30px;
                                    padding-top: 20px;
                                    border-top: 2px solid #f0f0f0;
                                }
                                .contact-info {
                                    color: #6c757d;
                                    font-size: 14px;
                                    margin: 20px 0;
                                }
                                @media (max-width: 600px) {
                                    .container {
                                        padding: 20px 15px;
                                        margin: 10px;
                                    }
                                    .invoice-details th,
                                    .invoice-details td {
                                        padding: 12px;
                                        font-size: 14px;
                                    }
                                    .total {
                                        font-size: 20px;
                                    }
                                }
                            </style>
                        </head>
                        <body>
                            <div class='container'>
                                <div class='header'>
                                    <h2>Ödeme Onayı</h2>
                                </div>
                                
                                <div class='greeting'>
                                    <p>Merhaba <strong>" + user.FullName + @"</strong>,</p>
                                    <p>Ödemeniz için teşekkür ederiz. Aşağıda ödeme detaylarınız bulunmaktadır:</p>
                                </div>

                                <table class='invoice-details'>
                                    <tr>
                                        <th>Fatura Tarihi</th>
                                        <td>" + DateTime.Now.ToString("yyyy-MM-dd") + @"</td>
                                    </tr>
                                    <tr>
                                        <th>Kiralama Süresi</th>
                                        <td>" + existReservationWithThisUser.StartDate.ToString("yyyy-MM-dd") + @" - " +
                                       existReservationWithThisUser.EndDate.ToString("yyyy-MM-dd") + @"</td>
                                    </tr>
                                    <tr>
                                        <th>Toplam Fiyat</th>
                                        <td><strong>" + existReservationWithThisUser.TotalPrice.ToString("F2") +
                                       @" TRY</strong></td>
                                    </tr>
                                </table>

                                <div class='total-section'>
                                    <p class='total'>Ödenen Tutar: " +
                                       existReservationWithThisUser.TotalPrice.ToString("F2") + @" TRY</p>
                                </div>

                                <div class='contact-info'>
                                    <p>Herhangi bir sorunuz olursa lütfen bizimle iletişime geçin.</p>
                                </div>

                                <div class='footer'>
                                    <p>&copy; 2025 Kuzeygo. Tüm Hakları Saklıdır.</p>
                                </div>
                            </div>
                        </body>
                        </html> ";

                        await _emailService.SendEmailAsync(user.Email, "Payment Confirmation", emailBody, true);                    
                }
                else
                {
                    throw new CustomException(400, "PaymentIntent", "Payment was unsuccessful");
                }
            }
            catch (StripeException ex)
            {
                throw new CustomException(400, "Stripe Payment Error", $"Stripe error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new CustomException(500, "Payment Processing Error", $"Exception: {ex.Message}");
            }
        }
    }
}