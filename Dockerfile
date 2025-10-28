# =========================
# Stage 1: Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the main project file and restore dependencies
COPY TechZone.Api/TechZoneV1/TechZoneV1.csproj TechZoneV1/
RUN dotnet restore TechZoneV1/TechZoneV1.csproj

# Copy the rest of the source code
COPY . .
RUN dotnet publish TechZoneV1/TechZoneV1.csproj -c Release -o /app/publish

# =========================
# Stage 2: Runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Railway automatically sets PORT
ENV ASPNETCORE_URLS=http://+:${PORT}

# PostgreSQL connection string (for Npgsql)
ENV ConnectionStrings__DefaultConnection="Host=${RAILWAY_TCP_HOST};Port=${RAILWAY_TCP_PORT};Database=mydb;Username=${RAILWAY_TCP_USER};Password=${RAILWAY_TCP_PASSWORD};SSL Mode=Prefer;Trust Server Certificate=True;"

ENTRYPOINT ["dotnet", "TechZoneV1.dll"]

