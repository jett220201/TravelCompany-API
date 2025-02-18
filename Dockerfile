# Using .NET SDK base image to compile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copying solution files and restoring dependencies
COPY TravelCompanyAPI.sln ./
COPY TravelCompanyAPI/TravelCompanyAPI.csproj TravelCompanyAPI/
RUN dotnet restore TravelCompanyAPI/TravelCompanyAPI.csproj

# Copy the rest of the code and compile
COPY . ./
WORKDIR /app/TravelCompanyAPI
RUN dotnet publish -c Release -o /app/out

# Use lighter .NET Runtime image to run
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Define entry point
ENTRYPOINT ["dotnet", "TravelCompanyAPI.dll"]
