FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StreakTracking.EventHandler/StreakTracking.EventHandler.csproj", "StreakTracking.EventHandler/"]
COPY ["StreakTracking.Infrastructure/StreakTracking.Infrastructure.csproj", "StreakTracking.Infrastructure/"]
COPY ["StreakTracking.Domain/StreakTracking.Domain.csproj", "StreakTracking.Domain/"]
COPY ["StreakTracking.Application/StreakTracking.Application.csproj", "StreakTracking.Application/"]
COPY ["StreakTracking.Messages/StreakTracking.Messages.csproj", "StreakTracking.Messages/"]
RUN dotnet restore "StreakTracking.EventHandler/StreakTracking.EventHandler.csproj"
COPY . .
WORKDIR "/src/StreakTracking.EventHandler"
RUN dotnet build "StreakTracking.EventHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StreakTracking.EventHandler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StreakTracking.EventHandler.dll"]
