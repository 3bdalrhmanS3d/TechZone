# =========================
# Stage 1: Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the csproj first (for better caching)
COPY TechZoneV1/TechZoneV1.csproj TechZoneV1/

# Restore dependencies
RUN dotnet restore TechZoneV1/TechZoneV1.csproj

# Copy the rest of the source code
COPY . .

# Build and publish the app
RUN dotnet publish TechZoneV1/TechZoneV1.csproj -c Release -o /app/publish


# =========================
# Stage 2: Runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/publish .

# Railway provides PORT → bind to it
ENV ASPNETCORE_URLS=http://+:${PORT}

# Optional: Default connection string (Railway overrides it)
ENV ConnectionStrings__DefaultConnection="Server=${RAILWAY_TCP_HOST},${RAILWAY_TCP_PORT};Database=mydb;User Id=${RAILWAY_TCP_USER};Password=${RAILWAY_TCP_PASSWORD};TrustServerCertificate=True;"

# Run the app
ENTRYPOINT ["dotnet", "TechZoneV1.dll"]
