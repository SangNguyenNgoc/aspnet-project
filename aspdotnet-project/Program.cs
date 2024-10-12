using aspdotnet_project.App.Auth.Services;
using aspdotnet_project.App.Cinema.Repositories;
using aspdotnet_project.App.Cinema.Services;
using aspdotnet_project.App.Movie;
using aspdotnet_project.App.Movie.Repositories;
using aspdotnet_project.App.Movie.Services;
using aspdotnet_project.App.Show.Repositories;
using aspdotnet_project.App.Show.Services;
using aspdotnet_project.App.User.Entities;
using aspdotnet_project.Context;
using aspdotnet_project.Filter;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

Env.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AppExceptionFilter());
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MovieProfile));

builder.Services.AddDbContext<MyDbContext>(options =>
{
    var server = Environment.GetEnvironmentVariable("DB_SERVER");
    var database = Environment.GetEnvironmentVariable("DB_NAME");
    var userId = Environment.GetEnvironmentVariable("DB_USER");
    var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

    var connectionString = $"Server={server};Database={database};User Id={userId};Password={password};";
    var mySqlVersion = new MySqlServerVersion(new Version(8, 0, 30));
    options.UseMySql(connectionString, mySqlVersion);
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;

        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<MyDbContext>();

builder.Services.AddAuthentication(options =>
{
    const string defaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = defaultScheme;
    options.DefaultChallengeScheme = defaultScheme;
    options.DefaultForbidScheme = defaultScheme;
    options.DefaultScheme = defaultScheme;
    options.DefaultSignInScheme = defaultScheme;
    options.DefaultSignOutScheme = defaultScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"] ?? "default-key") 
        )
    };
});

builder.Services.AddScoped<IAuthService, AuthService>();
// Cinema
builder.Services.AddScoped<IHallRepository, HallRepository>();
builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ICinemaService, CinemaService>();
// Movie
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IFormatRepository, FormatRepository>();
builder.Services.AddScoped<IMovieStatusRepository, MovieStatusRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
// Showtime
builder.Services.AddScoped<IShowtimeRepository, ShowtimeRepository>();
builder.Services.AddScoped<IShowtimeService, ShowtimeService>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseRouting();
app.MapControllers();
app.Run();