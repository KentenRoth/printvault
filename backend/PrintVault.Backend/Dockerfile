# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore dependencies first (leverages Docker layer caching)
COPY *.csproj ./
RUN dotnet restore

# Copy source code and publish
COPY . ./
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published application
COPY --from=build /app ./

# Configure environment
ENV ASPNETCORE_URLS=http://+:8080 \
    PRINTVAULT_DATA_ROOT=/data \
    PRINTVAULT_INCOMING=/data/incoming \
    PRINTVAULT_MODELS=/data/models

EXPOSE 8080

ENTRYPOINT ["dotnet", "PrintVault.Backend.dll"]
