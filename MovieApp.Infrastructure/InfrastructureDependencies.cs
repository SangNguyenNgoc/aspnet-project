using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Domain.Bill.Repositories;
using MovieApp.Domain.Cinema.Repositories;
using MovieApp.Domain.Movie.Repositories;
using MovieApp.Domain.Show.Repositories;
using MovieApp.Domain.User.Entities;
using MovieApp.Domain.User.Repositories;
using MovieApp.Infrastructure.Context; 
using MovieApp.Infrastructure.Mail;
using MovieApp.Infrastructure.Repositories.Bill;
using MovieApp.Infrastructure.Repositories.Cinema;
using MovieApp.Infrastructure.Repositories.Movie;
using MovieApp.Infrastructure.Repositories.Show;
using MovieApp.Infrastructure.Repositories.User;
using MovieApp.Infrastructure.VnPay;

namespace MovieApp.Infrastructure;

public static class InfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, 
        DbConfig dbConfig,
        MailConfig mailConfig,
        VnPayConfig vnPayConfig)
    { 
        
        services.AddDbContext<MyDbContext>(options =>
            { 
                var connectionString = $"Server={dbConfig.Server};Database={dbConfig.Database};User Id={dbConfig.User};Password={dbConfig.Password};";
                var mySqlVersion = new MySqlServerVersion(new Version(8, 0, 30));
                options.UseMySql(connectionString, mySqlVersion);
            });
        //Cinema
        services.AddScoped<IHallRepository, HallRepository>();
        services.AddScoped<ICinemaRepository, CinemaRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ISeatRepository, SeatRepository>();
        
        // Movie
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IFormatRepository, FormatRepository>();
        services.AddScoped<IMovieStatusRepository, MovieStatusRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        
        //Bill
        services.AddScoped<IBillStatusRepository, BillStatusRepository>();
        services.AddScoped<IBillRepository, BillRepository>();
        
        // Showtime
        services.AddScoped<IShowtimeRepository, ShowtimeRepository>();
        
        // User
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultProvider;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
                options.Tokens.ChangeEmailTokenProvider = TokenOptions.DefaultProvider;

            })
            .AddEntityFrameworkStores<MyDbContext>()
            .AddDefaultTokenProviders();
        
        services.AddSingleton(new EmailService(mailConfig));
        services.AddSingleton(vnPayConfig);
        
        return services;
    }
}