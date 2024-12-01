using MovieApp.Application.Feature.Dashboard.Dtos;

namespace MovieApp.Application.Feature.Dashboard.Services;

public interface IDashboardService
{
    Task<List<Chart>> GetDashboard(int year);
    Task<List<MovieDashboard>?> StatisticMovie(int month, int year);
    Task<List<CinemaDashboard>?> StatisticCinema(int month, int year);
    Task<StatisticsOfTime> GetStatisticsOfTime(DateTime from, DateTime to);
}