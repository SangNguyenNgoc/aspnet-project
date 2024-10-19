using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Identity.Services;

namespace MovieApp.Identity;

public static class IdentityDependencies
{
    public static void AddIdentityDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
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
                ValidIssuer = configuration["JWT:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["JWT:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"] ?? "default-key")
                )
            };
        });

        services.AddScoped<IAuthService, AuthService>();
    }
}