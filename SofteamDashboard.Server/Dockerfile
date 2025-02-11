# Use the official ASP.NET 8.0 runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG APP_UID=1000  # You can replace this with your actual user ID if needed
RUN adduser --uid $APP_UID --disabled-password --gecos "" appuser
USER appuser
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK 8.0 for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SofteamDashboard.Server/SofteamDashboard.Server.csproj", "SofteamDashboard.Server/"]
COPY ["SofteamDashboard.Core/SofteamDashboard.Core.csproj", "SofteamDashboard.Core/"]
RUN dotnet restore "SofteamDashboard.Server/SofteamDashboard.Server.csproj"
COPY . .
WORKDIR "/src/SofteamDashboard.Server"
RUN dotnet build "SofteamDashboard.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SofteamDashboard.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image: copy from the publish stage and set the entry point
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SofteamDashboard.Server.dll"]
