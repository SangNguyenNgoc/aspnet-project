using DotNetEnv;
using Microsoft.OpenApi.Models;
using MovieApp.Api.Filter;
using MovieApp.Application;
using MovieApp.Identity;
using MovieApp.Infrastructure;
using MovieApp.Infrastructure.Context;
using MovieApp.Infrastructure.Mail;
using MovieApp.Infrastructure.S3;
using MovieApp.Infrastructure.VnPay;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers(options => { options.Filters.Add(new AppExceptionFilter()); });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var vnPayConfig = new VnPayConfig
{
    VnpayKey = Environment.GetEnvironmentVariable("VNPAY_KEY")!,
    VnpayUrl = Environment.GetEnvironmentVariable("VNPAY_URL")!,
    TmnCode = Environment.GetEnvironmentVariable("TMN_CODE")!,
    VnpayReturnUrl = Environment.GetEnvironmentVariable("VNPAY_RETURN_URL")!,
    TimeOut = int.Parse(Environment.GetEnvironmentVariable("VNPAY_TIMEOUT")!),
};
var dbConfig = new DbConfig
{
    Server = Environment.GetEnvironmentVariable("DB_SERVER")!,
    Database = Environment.GetEnvironmentVariable("DB_NAME")!,
    User = Environment.GetEnvironmentVariable("DB_USER")!,
    Password = Environment.GetEnvironmentVariable("DB_PASSWORD")!
};

var mailConfig = new MailConfig
{
    Host = Environment.GetEnvironmentVariable("STMP_SERVER")!,
    Port = int.Parse(Environment.GetEnvironmentVariable("STMP_PORT")!),
    Username = Environment.GetEnvironmentVariable("STMP_USERNAME")!,
    Password = Environment.GetEnvironmentVariable("STMP_PASSWORD")!
};

var s3Config = new S3Config
{
    AccessKey = Environment.GetEnvironmentVariable("S3_ACCESS_KEY")!,
    SecretKey = Environment.GetEnvironmentVariable("S3_SECRET_KEY")!,
    EndpointUrl = Environment.GetEnvironmentVariable("ENDPOINT")!,
    Region = Environment.GetEnvironmentVariable("REGION")!
};

builder.Services.AddInfrastructureDependencies(dbConfig, mailConfig, vnPayConfig, s3Config);

builder.Services.AddApplicationDependencies();

builder.Services.AddIdentityDependencies(builder.Configuration);

builder.WebHost.ConfigureKestrel(serverOptions => { serverOptions.ListenAnyIP(8080); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAll");

app.UseRouting();
app.MapControllers();
app.Run();