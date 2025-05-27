FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["backend/ScheduleService/ScheduleService.Api/ScheduleService.Api.csproj", "backend/ScheduleService/ScheduleService.Api/"]
COPY ["backend/ScheduleService/ScheduleService.DomainModel/ScheduleService.DomainModel.csproj", "backend/ScheduleService/ScheduleService.DomainModel/"]
COPY ["backend/ScheduleService/ScheduleService.Application/ScheduleService.Application.csproj", "backend/ScheduleService/ScheduleService.Application/"]
COPY ["backend/ScheduleService/ScheduleService.DataAccess/ScheduleService.DataAccess.csproj", "backend/ScheduleService/ScheduleService.DataAccess/"]
RUN dotnet restore "backend/ScheduleService/ScheduleService.Api/ScheduleService.Api.csproj"
COPY . .
WORKDIR "/src/backend/ScheduleService/ScheduleService.Api"
RUN dotnet build "ScheduleService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ScheduleService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScheduleService.Api.dll"]
