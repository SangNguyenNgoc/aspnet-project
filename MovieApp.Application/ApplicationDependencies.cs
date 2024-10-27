using Microsoft.Extensions.DependencyInjection;
using MovieApp.Application.BackgroundTasks.UpdateBill;
using MovieApp.Application.Feature.Bill.Services;
using MovieApp.Application.Feature.Cinema.Services;
using MovieApp.Application.Feature.Movie;
using MovieApp.Application.Feature.Movie.Services;
using MovieApp.Application.Feature.Show.Services;
using MovieApp.Application.Feature.User.Service;
using Quartz;

namespace MovieApp.Application;

public static class ApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddAutoMapper(typeof(MovieProfile));
        
        services.AddScoped<ICinemaService, CinemaService>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IShowtimeService, ShowtimeService>();
        services.AddScoped<IBillService, BillService>();
        services.AddScoped<IUserService, UserService>();

        services.AddQuartz();
        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
            options.AwaitApplicationStarted = true;
        });

        services.ConfigureOptions<UpdateBillStatusSetup>();
        
        return services;
    } 
}