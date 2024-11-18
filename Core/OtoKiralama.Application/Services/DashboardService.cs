using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.Car;
using OtoKiralama.Application.Dtos.Dashboard;
using OtoKiralama.Application.Dtos.Reservation;
using OtoKiralama.Application.Dtos.User;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Enums;
using OtoKiralama.Persistance.Data;
using OtoKiralama.Persistance.Data.Implementations;
using OtoKiralama.Persistance.Entities;

namespace OtoKiralama.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IMapper mapper, AppDbContext context, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<DashboardStatisticsDto> GetDashboardData()
        {
            var reservations = await _unitOfWork.ReservationRepository.GetAll(
                includes: query => query
                    .Include(r => r.Car)
                    .Include(r => r.AppUser)
            );
            var mappedReservations = _mapper.Map<List<ReservationListItemDto>>(reservations);
            int totalCarCount = await _context.Cars.CountAsync();
            int totalBrandCount = await _context.Brands.CountAsync();
            int totalModelCount = await _context.Models.CountAsync();
            int totalCompanyCount = await _context.Companies.CountAsync();
            int totalUserCount = await _userManager.Users.CountAsync();
            int totalPhotoCount = await _context.CarPhotos.CountAsync();
            int totalReservationCount = mappedReservations.Count(r => r.IsPaid==true);
            int completedReservations = mappedReservations.Count(r => r.Status == ReservationStatus.Completed && r.IsPaid && !r.IsCanceled);
            int canceledReservations = mappedReservations.Count(r=>r.IsPaid && r.Status==ReservationStatus.Canceled);
            int ongoingReservations = mappedReservations.Count(r=>r.IsCanceled==false && r.IsPaid && r.Status == ReservationStatus.InProgress);
            int pendingReservations = mappedReservations.Count(r=>r.IsPaid && !r.IsCanceled && r.Status == ReservationStatus.Pending);

            double monthlyEarnedMoney = mappedReservations.Where(r => r.IsPaid && !r.IsCanceled && r.StartDate.Month == DateTime.Now.Month).Sum(r => r.TotalPrice);
            double yearlyEarnedMoney = mappedReservations.Where(r => r.IsPaid && !r.IsCanceled && r.StartDate.Year == DateTime.Now.Year).Sum(r => r.TotalPrice);
            double totalEarnedMoney = mappedReservations.Where(r => r.IsPaid && !r.IsCanceled).Sum(r => r.TotalPrice);

            int monthlyNewUserCount = await _userManager.Users.CountAsync(u => u.CreatedDate.Month == DateTime.Now.Month);
            int yearlyNewUserCount = await _userManager.Users.CountAsync(u => u.CreatedDate.Year == DateTime.Now.Year);
            int monthlyNewReservationCount = mappedReservations.Count(r => r.StartDate.Month == DateTime.Now.Month && !r.IsCanceled);
            int previousMonthNewReservationCount = mappedReservations.Count(r => r.StartDate.Month == DateTime.Now.Month - 1 && !r.IsCanceled && r.IsPaid);
            double reservationGrowthPercentage = previousMonthNewReservationCount == 0 ? 0 : (double)(monthlyNewReservationCount - previousMonthNewReservationCount) / previousMonthNewReservationCount * 100;

            int previousMonthUserCount = await _userManager.Users.CountAsync(u => u.CreatedDate.Month == DateTime.Now.Month - 1);
            double userGrowthPercentage = previousMonthUserCount == 0 ? 0 : (double)(monthlyNewUserCount - previousMonthUserCount) / previousMonthUserCount * 100;

            int previousYearUserCount = await _userManager.Users.CountAsync(u => u.CreatedDate.Year == DateTime.Now.Year - 1);
            double userGrowthPercentageFromPreviousYear = previousYearUserCount == 0 ? 0 : (double)(yearlyNewUserCount - previousYearUserCount) / previousYearUserCount * 100;

            int previousMonthCompanyCount = await _context.Companies.CountAsync(c => c.CreatedDate.Month == DateTime.Now.Month - 1);
            double companyGrowthPercentage = previousMonthCompanyCount == 0 ? 0 : (double)(totalCompanyCount - previousMonthCompanyCount) / previousMonthCompanyCount * 100;

            int previousYearCompanyCount = await _context.Companies.CountAsync(c => c.CreatedDate.Year == DateTime.Now.Year - 1);
            double companyGrowthPercentageFromPreviousYear = previousYearCompanyCount == 0 ? 0 : (double)(totalCompanyCount - previousYearCompanyCount) / previousYearCompanyCount * 100;

            double previousMonthEarnedMoney = mappedReservations.Where(r => r.IsPaid && !r.IsCanceled && r.StartDate.Month == DateTime.Now.Month - 1).Sum(r => r.TotalPrice);
            double thisMonthEarnedMoney = mappedReservations.Where(r => r.IsPaid && !r.IsCanceled && r.StartDate.Month == DateTime.Now.Month).Sum(r => r.TotalPrice);
            double earnedMoneyDifference = thisMonthEarnedMoney - previousMonthEarnedMoney;

            double previousYearEarnedMoney = mappedReservations.Where(r => r.IsPaid && !r.IsCanceled && r.StartDate.Year == DateTime.Now.Year - 1).Sum(r => r.TotalPrice);
            double thisYearEarnedMoney = mappedReservations.Where(r => r.IsPaid && !r.IsCanceled && r.StartDate.Year == DateTime.Now.Year).Sum(r => r.TotalPrice);
            double earnedMoneyDifferenceFromPreviousYear = thisYearEarnedMoney - previousYearEarnedMoney;

            return new DashboardStatisticsDto()
            {
                CanceledReservations = canceledReservations,
                CompletedReservations = completedReservations,
                CompanyGrowthPercentage = companyGrowthPercentage,
                CompanyGrowthPercentageFromPreviousYear = companyGrowthPercentageFromPreviousYear,
                MonthlyEarnedMoney = monthlyEarnedMoney,
                MonthlyNewReservationCount = monthlyNewReservationCount,
                MonthlyNewUserCount = monthlyNewUserCount,
                OngoingReservations = ongoingReservations,
                PendingReservations = pendingReservations,
                PreviousMonthCompanyCount = previousMonthCompanyCount,
                PreviousMonthEarnedMoney = previousMonthEarnedMoney,
                PreviousMonthReservationCount = previousMonthNewReservationCount,
                PreviousMonthUserCount = previousMonthUserCount,
                PreviousYearCompanyCount = previousYearCompanyCount,
                PreviousYearEarnedMoney = previousYearEarnedMoney,
                PreviousYearUserCount = previousYearUserCount,
                ReservationGrowthPercentage = reservationGrowthPercentage,
                ThisMonthEarnedMoney = thisMonthEarnedMoney,
                ThisYearEarnedMoney = thisYearEarnedMoney,
                TotalBrandCount = totalBrandCount,
                TotalCarCount = totalCarCount,
                TotalCompanyCount = totalCompanyCount,
                TotalEarnedMoney = totalEarnedMoney,
                TotalModelCount = totalModelCount,
                TotalPhotoCount = totalPhotoCount,
                TotalReservationCount = totalReservationCount,
                TotalUserCount = totalUserCount,
                UserGrowthPercentage = userGrowthPercentage,
                UserGrowthPercentageFromPreviousYear = userGrowthPercentageFromPreviousYear,
                YearlyEarnedMoney = yearlyEarnedMoney,
                YearlyNewUserCount = yearlyNewUserCount,
                EarnedMoneyDifference = earnedMoneyDifference,
                EarnedMoneyDifferenceFromPreviousYear = earnedMoneyDifferenceFromPreviousYear
            };
        }
    }
}
