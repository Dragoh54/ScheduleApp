FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["backend/UserService/UserService.Api/UserService.Api.csproj", "backend/UserService/UserService.Api/"]
COPY ["backend/UserService/UserService.DataAccess/UserService.DataAccess.csproj", "backend/UserService/UserService.DataAccess/"]
COPY ["backend/UserService/UserService.Application/UserService.Application.csproj", "backend/UserService/UserService.Application/"]
RUN dotnet restore "backend/UserService/UserService.Api/UserService.Api.csproj"
COPY . .
WORKDIR "/src/backend/UserService/UserService.Api"
RUN dotnet build "UserService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.Api.dll"]
