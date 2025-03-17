FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["backend/MeetingService/MeetingService.Api/MeetingService.Api.csproj", "backend/MeetingService/MeetingService.Api/"]
RUN dotnet restore "backend/MeetingService/MeetingService.Api/MeetingService.Api.csproj"
COPY . .
WORKDIR "/src/backend/MeetingService/MeetingService.Api"
RUN dotnet build "MeetingService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MeetingService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetingService.Api.dll"]
