FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files
COPY src/TaskManager.Api/*.csproj src/TaskManager.Api/
COPY src/TaskManager.Application/*.csproj src/TaskManager.Application/
COPY src/TaskManager.Domain/*.csproj src/TaskManager.Domain/
COPY src/TaskManager.Infrastructure/*.csproj src/TaskManager.Infrastructure/

# Restore dependencies
RUN dotnet restore src/TaskManager.Api/TaskManager.Api.csproj

# Copy the rest of the files
COPY src/. src/

# Build and publish
RUN dotnet publish src/TaskManager.Api/TaskManager.Api.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5000

EXPOSE 5000

ENTRYPOINT ["dotnet", "TaskManager.Api.dll"]
