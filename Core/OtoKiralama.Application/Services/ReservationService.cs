using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.Car;
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

        public async Task CreateReservationAsync(ReservationCreateDto reservationCreateDto)
        {
            var existCar = await _unitOfWork.CarRepository.isExists(c => c.Id == reservationCreateDto.CarId);
            if (!existCar)
                throw new CustomException(404, "CarId", "Car not found with this Id");
            var existUser = await _userManager.FindByIdAsync(reservationCreateDto.UserId);
            if (existUser is null)
                throw new CustomException(404, "UserId", "User not found with this Id");
            bool isCarAvailable = await IsCarAvailable(reservationCreateDto.CarId, reservationCreateDto.StartDate, reservationCreateDto.EndDate);
            if (!isCarAvailable)
                throw new CustomException(400, "CarId", "Car is not available for this date range");
            var reservation = _mapper.Map<Reservation>(reservationCreateDto);
            reservation.AppUserId = reservationCreateDto.UserId;
            reservation.TotalPrice = (reservation.EndDate - reservation.StartDate).TotalDays * reservation.Car.DailyPrice;
            await _unitOfWork.ReservationRepository.Create(reservation);
            _unitOfWork.Commit();
        }

        public async Task DeleteReservationAsync(int id)
        {
            var existReservation = await _unitOfWork.ReservationRepository.GetEntity(r=>r.Id==id);
            if (existReservation is null)
                throw new CustomException(400, "Id", "Reservation with this Id is not found");
            await _unitOfWork.ReservationRepository.Delete(existReservation);
            _unitOfWork.Commit();
        }

        public async Task<PagedResponse<ReservationListItemDto>> GetAllReservationsAsync(int pageNumber, int pageSize)
        {
            var totalReservations = await _unitOfWork.ReservationRepository.CountAsync();
            var reservations = await _unitOfWork.ReservationRepository.GetAll(
                includes: query => query
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Brand)
                    .Include(c => c.Car)
                        .ThenInclude(c => c.Model)
                            .ThenInclude(m=> m.CarPhoto)
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

        public async Task<bool> IsCarAvailable(int carId, DateTime startDate, DateTime endDate)
        {
            var reservations = await _unitOfWork.ReservationRepository.GetAll(
                predicate: r => r.CarId == carId &&
                              ((r.StartDate >= startDate && r.StartDate <= endDate) ||
                               (r.EndDate >= startDate && r.EndDate <= endDate) ||
                               (r.StartDate <= startDate && r.EndDate >= endDate))
            );

            return reservations.Count == 0;
        }
    }
}
