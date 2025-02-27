using Microsoft.AspNetCore.Identity;
using OtoKiralama.Application.Dtos.Payment;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
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

        public PaymentService(IUserResolverService appUserResolver, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _appUserResolver = appUserResolver;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task Pay(PaymentRequestDto paymentRequestDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            Reservation existReservationWithThisUser = null;
            
            try
            {
                var existUserId = await _appUserResolver.GetCurrentUserIdAsync();
                if (string.IsNullOrWhiteSpace(existUserId))
                    throw new CustomException(400, "User", "User is not authenticated");

                var user = await _userManager.FindByIdAsync(existUserId);
                if (user == null)
                    throw new CustomException(400, "User", "User is not authenticated");

                existReservationWithThisUser = await _unitOfWork.ReservationRepository.GetEntity(r => r.Id == paymentRequestDto.ReservationId && r.AppUserId == user.Id);
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

                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = await paymentIntentService.CreateAsync(paymentIntentOptions);

                if (paymentIntent.Status == "succeeded")
                {
                    existReservationWithThisUser.IsPaid = true;
                    await _unitOfWork.ReservationRepository.Update(existReservationWithThisUser);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitTransactionAsync();
                }
                else
                    throw new CustomException(400, "PaymentIntent", "Payment was unsuccessful");
            }
            catch (StripeException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();

                var message = $"Stripe error: {ex.Message}, Inner: {ex.InnerException?.Message ?? "None"}";

                if (existReservationWithThisUser != null)
                {
                    await _unitOfWork.ReservationRepository.Delete(existReservationWithThisUser);
                    await _unitOfWork.SaveChangesAsync();
                }

                throw new CustomException(400, "Stripe Payment Error", message);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();

                var message = $"Exception: {ex.Message}, Inner: {ex.InnerException?.Message ?? "None"}";
                throw new CustomException(500, "Payment Processing Error", message);
            }
        }
    }
}