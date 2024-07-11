# Use the official .NET image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ../Coupon.API.sln .
COPY Coupon.Domain/*.csproj ./Coupon.Domain/
COPY Coupon.Application/*.csproj ./Coupon.Application/
COPY Coupon.Infrastructure/*.csproj ./Coupon.Infrastructure/
COPY Coupon.API/*.csproj ./Coupon.API/
RUN dotnet restore

# Copy everything else and build
COPY Coupon.Domain ./Coupon.Domain
COPY Coupon.Application ./Coupon.Application
COPY Coupon.Infrastructure ./Coupon.Infrastructure
COPY Coupon.API ./Coupon.API
WORKDIR /app/Coupon.API
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/Coupon.API/out .
ENTRYPOINT ["dotnet", "Coupon.API.dll"]