# =========================
# Stage 1: Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the main project file
COPY TechZone/TechZone.Api/TechZone.Api.csproj TechZone/TechZone.Api/

# Restore dependencies
RUN dotnet restore TechZone/TechZone.Api/TechZone.Api.csproj

# Copy the rest of the source code
COPY . .

# Build and publish
RUN dotnet publish TechZone/TechZone.Api/TechZone.Api.csproj -c Release -o /app/publish


# =========================
# Stage 2: Runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Railway provides PORT automatically
ENV ASPNETCORE_URLS=http://+:${PORT}

# Optional connection string (Railway overrides it)
ENV ConnectionStrings__DefaultConnection="Server=${RAILWAY_TCP_HOST},${RAILWAY_TCP_PORT};Database=mydb;User Id=${RAILWAY_TCP_USER};Password=${RAILWAY_TCP_PASSWORD};TrustServerCertificate=True;"

# Run the app
ENTRYPOINT ["dotnet", "TechZone.Api.dll"]
