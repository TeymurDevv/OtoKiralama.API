namespace OtoKiralama.Application.Dtos.Dashboard
{
    public class DashboardStatisticsDto
    {
        public int TotalCarCount { get; set; }
        public int TotalBrandCount { get; set; }
        public int TotalModelCount { get; set; }
        public int TotalCompanyCount { get; set; }
        public int TotalUserCount { get; set; }
        public int TotalPhotoCount { get; set; }
        public int TotalReservationCount { get; set; }
        public int CompletedReservations { get; set; }
        public int PendingReservations { get; set; }
        public int CanceledReservations { get; set; }
        public int OngoingReservations { get; set; }
        public double MonthlyEarnedMoney { get; set; }
        public double YearlyEarnedMoney { get; set; }
        public double TotalEarnedMoney { get; set; }
        public int MonthlyNewUserCount { get; set; }
        public int YearlyNewUserCount { get; set; }
        public int MonthlyNewReservationCount { get; set; }
        public int PreviousMonthReservationCount { get; set; }
        public double ReservationGrowthPercentage { get; set; }
        public int PreviousMonthUserCount { get; set; }
        public double UserGrowthPercentage { get; set; }
        public int PreviousYearUserCount { get; set; }
        public double UserGrowthPercentageFromPreviousYear { get; set; }
        public int PreviousMonthCompanyCount { get; set; }
        public int PreviousYearCompanyCount { get; set; }
        public double CompanyGrowthPercentage { get; set; }
        public double CompanyGrowthPercentageFromPreviousYear { get; set; }
        public double PreviousMonthEarnedMoney { get; set; }
        public double ThisMonthEarnedMoney { get; set; }
        public double EarnedMoneyDifference { get; set; }
        public double PreviousYearEarnedMoney { get; set; }
        public double ThisYearEarnedMoney { get; set; }
        public double EarnedMoneyDifferenceFromPreviousYear { get; set; }

    }
}
