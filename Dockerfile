# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only the .csproj file first (for restore layer)
COPY TechZone.Api/TechZoneV1.csproj ./TechZone.Api/
RUN dotnet restore ./TechZone.Api/TechZoneV1.csproj

# Copy the rest of the project files
COPY TechZone.Api/. ./TechZone.Api/
WORKDIR /app/TechZone.Api

# Build and publish the app
RUN dotnet publish -c Release -o out

# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/TechZone.Api/out .
ENTRYPOINT ["dotnet", "TechZoneV1.dll"]
