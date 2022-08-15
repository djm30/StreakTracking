FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StreakTracking.API/StreakTracking.API.csproj", "StreakTracking.API/"]
COPY ["StreakTracking.Infrastructure/StreakTracking.Infrastructure.csproj", "StreakTracking.Infrastructure/"]
COPY ["StreakTracking.Domain/StreakTracking.Domain.csproj", "StreakTracking.Domain/"]
COPY ["StreakTracking.Application/StreakTracking.Application.csproj", "StreakTracking.Application/"]
COPY ["StreakTracking.Messages/StreakTracking.Messages.csproj", "StreakTracking.Messages/"]
RUN dotnet restore "StreakTracking.API/StreakTracking.API.csproj"
COPY . .
WORKDIR "/src/StreakTracking.API"
RUN dotnet build "StreakTracking.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StreakTracking.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StreakTracking.API.dll"]
