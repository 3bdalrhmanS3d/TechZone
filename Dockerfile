# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files first (better caching of restore step)
# COPY TechZone.Core/TechZone.Core.csproj TechZone.Core/
# COPY TechZone.EF/TechZone.EF.csproj TechZone.EF/
 COPY TechZoneV1/TechZoneV1.csproj TechZoneV1/

# Restore dependencies
RUN dotnet restore TechZoneV1/TechZoneV1.csproj

# Copy everything else
COPY . .

# Build and publish API project
RUN dotnet publish TechZoneV1/TechZoneV1.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Railway provides PORT → bind to it
ENV ASPNETCORE_URLS=http://+:${PORT}

# Optional: Default connection string (overwritten by Railway env)
# If using Railway SQL Server plugin, Railway injects RAILWAY_TCP_* env vars
ENV ConnectionStrings__DefaultConnection="Server=${RAILWAY_TCP_HOST},${RAILWAY_TCP_PORT};Database=mydb;User Id=${RAILWAY_TCP_USER};Password=${RAILWAY_TCP_PASSWORD};TrustServerCertificate=True;"

ENTRYPOINT ["dotnet", "TechZoneV1.dll"]
