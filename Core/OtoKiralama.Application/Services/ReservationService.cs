using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Dtos.Reservation;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Data.Implementations;
using OtoKiralama.Persistance.Entities;

namespace OtoKiralama.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task CancelReservation(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var existReservation = await _unitOfWork.ReservationRepository.GetEntity(r => r.Id == id);
                if (existReservation is null)
                    throw new CustomException(404, "Id", "Reservation not found with this Id");

                if (existReservation.Status == Domain.Enums.ReservationStatus.Canceled)
                    throw new CustomException(400, "Id", "Cannot cancel a reservation that is already canceled");

                existReservation.IsCanceled = true;

                var existCar = await _unitOfWork.CarRepository.GetEntity(c => c.Id == existReservation.CarId);
                existCar.IsReserved = false;

                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task CompleteReservation(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var existReservation = await _unitOfWork.ReservationRepository.GetEntity(r => r.Id == id);
                if (existReservation is null)
                    throw new CustomException(404, "Id", "Reservation not found with this Id");

                if (existReservation.Status == Domain.Enums.ReservationStatus.Canceled)
                    throw new CustomException(400, "Id", "Cannot complete a reservation that is canceled");

                if (existReservation.Status == Domain.Enums.ReservationStatus.Completed)
                    throw new CustomException(400, "Id", "Reservation is already completed");

                existReservation.IsCompleted = true;

                var existCar = await _unitOfWork.CarRepository.GetEntity(c => c.Id == existReservation.CarId);
                existCar.IsReserved = false;

                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task CreateReservationAsync(ReservationCreateDto reservationCreateDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var existCar = await _unitOfWork.CarRepository.isExists(c => c.Id == reservationCreateDto.CarId);
                if (!existCar)
                    throw new CustomException(404, "CarId", "Car not found with this Id");

                var existCarEntity = await _unitOfWork.CarRepository.GetEntity(c => c.Id == reservationCreateDto.CarId);
                if (existCarEntity.IsReserved)
                    throw new CustomException(400, "CarId", "Car is already reserved");

                if (!existCarEntity.IsActive)
                    throw new CustomException(400, "CarId", "Car is not active");

                var existUser = await _userManager.FindByIdAsync(reservationCreateDto.UserId);
                if (existUser is null)
                    throw new CustomException(404, "UserId", "User not found with this Id");

                var reservation = _mapper.Map<Reservation>(reservationCreateDto);
                reservation.AppUserId = reservationCreateDto.UserId;
                reservation.TotalPrice = (reservation.EndDate - reservation.StartDate).TotalDays * existCarEntity.DailyPrice;
                existCarEntity.IsReserved = true;

                await _unitOfWork.ReservationRepository.Create(reservation);
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task DeleteReservationAsync(int id)
        {
            var existReservation = await _unitOfWork.ReservationRepository.GetEntity(r => r.Id == id);
            if (existReservation is null)
                throw new CustomException(400, "Id", "Reservation with this Id is not found");

            if (existReservation.Status == Domain.Enums.ReservationStatus.InProgress)
                throw new CustomException(400, "Id", "Cannot delete a reservation that is in progress");

            await _unitOfWork.ReservationRepository.Delete(existReservation);
            await _unitOfWork.CommitAsync();
        }

        public async Task<PagedResponse<ReservationListItemDto>> GetAllReservationsAsync(int pageNumber, int pageSize)
        {
            var totalReservations = await _unitOfWork.ReservationRepository.CountAsync();
            var reservations = await _unitOfWork.ReservationRepository.GetAll(
                includes: query => query
                    .Include(c => c.Car)
                    .ThenInclude(c => c.Model)
                        .ThenInclude(c => c.Brand)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Model)
                            .ThenInclude(m => m.CarPhoto)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Body)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Class)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Fuel)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Gear)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Location)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Company)
                    .Include(c => c.AppUser)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
            );

            return new PagedResponse<ReservationListItemDto>
            {
                Data = _mapper.Map<List<ReservationListItemDto>>(reservations),
                TotalCount = totalReservations,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<ReservationReturnDto> GetReservationByIdAsync(int id)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetEntity(
                r => r.Id == id,
                includes: query => query
                    .Include(c => c.Car)
                    .ThenInclude(c => c.Model)
                        .ThenInclude(c => c.Brand)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Model)
                            .ThenInclude(m => m.CarPhoto)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Body)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Class)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Fuel)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Gear)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Location)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Company)
                    .Include(c => c.AppUser)
            );

            if (reservation is null)
                throw new CustomException(404, "Id", "Reservation not found with this Id");

            return _mapper.Map<ReservationReturnDto>(reservation);
        }
    }
}
