using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Application.BackgroundServices
{
    public class ReservationDeleteBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<ReservationDeleteBackgroundService> _logger;
        public ReservationDeleteBackgroundService(IServiceProvider services, ILogger<ReservationDeleteBackgroundService> logger)
        {
            _services = services;
            _logger = logger;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    DateTime now = DateTime.Now;
                    if (now.Day == 1)
                    {
                        _logger.LogInformation("Triggering ReservationDeleteBackgroundService for All reservations.");
                        await DeleteAllUnPaidReservations(stoppingToken);
                    }

                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); 

                }
                catch (TaskCanceledException)
                {
                    _logger.LogWarning("ReservationDeleteBackgroundService stopped before next execution.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred in ReservationDeleteBackgroundService");
                }
            }

            _logger.LogInformation("ReservationDeleteBackgroundService Stopped.");
        }
        private async Task DeleteAllUnPaidReservations(CancellationToken stoppingToken)
        {
            using (var scope = _services.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
             await unitOfWork.BeginTransactionAsync(stoppingToken);
                try
                {
                    var unpaidReservations = await unitOfWork.ReservationRepository.GetAll(s => s.IsPaid == false);
                    foreach(var unpaidReservation in unpaidReservations)
                    {
                        var carInReservation = await unitOfWork.CarRepository.GetEntity(s => s.Id == unpaidReservation.CarId);
                        carInReservation.IsReserved = false;
                        await unitOfWork.ReservationRepository.Delete(unpaidReservation);
                        await unitOfWork.CarRepository.Update(carInReservation);
                        await unitOfWork.CommitTransactionAsync(stoppingToken);
                        await unitOfWork.SaveChangesAsync();

                    }
                }
                catch (Exception ex)
                {
                    await unitOfWork.RollbackTransactionAsync(stoppingToken);
                    _logger.LogError(ex, "Error occurred while deleting and updating.");
                }
            }

        }
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ReservationDeleteBackgroundService stopping.");
            await base.StopAsync(stoppingToken);
        }
    }
}
