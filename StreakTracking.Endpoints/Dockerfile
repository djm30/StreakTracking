FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StreakTracking.Endpoints/Endpoints.API", "StreakTracking.Endpoints/Endpoints.API/"]
COPY ["StreakTracking.Endpoints/Endpoints.Infrastructure", "StreakTracking.Endpoints/Endpoints.Infrastructure/"]
COPY ["StreakTracking.Endpoints/Endpoints.Application", "StreakTracking.Endpoints/Endpoints.Application/"]
COPY ["StreakTracking.Messages/", "StreakTracking.Messages/"]
COPY ["StreakTracking.Domain/", "StreakTracking.Domain/"]
COPY ["StreakTracking.Common/", "StreakTracking.Common/"]

RUN dotnet restore "StreakTracking.Endpoints/Endpoints.API/Endpoints.API.csproj"


WORKDIR "StreakTracking.Endpoints/Endpoints.API/"
RUN dotnet build "Endpoints.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Endpoints.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Endpoints.API.dll"]
