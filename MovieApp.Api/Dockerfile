# Cơ sở
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Giai đoạn build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Sao chép các tệp csproj của tất cả các project
COPY ["MovieApp.Api/MovieApp.Api.csproj", "MovieApp.Api/"]
COPY ["MovieApp.Application/MovieApp.Application.csproj", "MovieApp.Application/"]
COPY ["MovieApp.Domain/MovieApp.Domain.csproj", "MovieApp.Domain/"]
COPY ["MovieApp.Identity/MovieApp.Identity.csproj", "MovieApp.Identity/"]
COPY ["MovieApp.Infrastructure/MovieApp.Infrastructure.csproj", "MovieApp.Infrastructure/"]

# Khôi phục dependencies
RUN dotnet restore "MovieApp.Api/MovieApp.Api.csproj"

# Sao chép toàn bộ mã nguồn vào image
COPY . .

# Xây dựng project
WORKDIR "/src/MovieApp.Api" 
# Chuyển đến thư mục chứa project API
RUN dotnet build "MovieApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Giai đoạn publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MovieApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Giai đoạn final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "MovieApp.Api.dll"]

