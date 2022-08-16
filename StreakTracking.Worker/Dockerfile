FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StreakTracking.Worker/Worker.EventHandler", "StreakTracking.Worker/Worker.EventHandler/"]
COPY ["StreakTracking.Worker/Worker.Infrastructure", "StreakTracking.Worker/Worker.Infrastructure/"]
COPY ["StreakTracking.Worker/Worker.Application", "StreakTracking.Worker/Worker.Application/"]
COPY ["StreakTracking.Messages/", "StreakTracking.Messages/"]
COPY ["StreakTracking.Domain/", "StreakTracking.Domain/"]
COPY ["StreakTracking.Common/", "StreakTracking.Common/"]

RUN dotnet restore "StreakTracking.Worker/Worker.EventHandler/Worker.EventHandler.csproj"

WORKDIR "StreakTracking.Worker/Worker.EventHandler/"
RUN dotnet build "Worker.EventHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Worker.EventHandler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Worker.EventHandler.dll"]
