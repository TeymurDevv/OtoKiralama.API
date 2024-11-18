using OtoKiralama.Application.Dtos.Dashboard;

namespace OtoKiralama.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardStatisticsDto> GetDashboardData();
    }
}
