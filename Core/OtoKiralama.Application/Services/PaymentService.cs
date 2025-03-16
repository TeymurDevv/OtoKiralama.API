using Microsoft.AspNetCore.Identity;
using OtoKiralama.Application.Dtos.Payment;
using OtoKiralama.Application.Exceptions;
using Microsoft.Extensions.Options;
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

        public PaymentService(IUserResolverService appUserResolver, UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IOptions<StripeSettings> stripeSettings)
        {
            _appUserResolver = appUserResolver;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
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