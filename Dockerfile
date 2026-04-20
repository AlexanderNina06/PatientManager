FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Install Node (required by npm scripts run during publish)
# some projects call `npm run css:build` during dotnet publish; install Node.js so those commands exist
RUN apt-get update \
    && apt-get install -y curl ca-certificates gnupg \
    && curl -fsSL https://deb.nodesource.com/setup_18.x | bash - \
    && apt-get install -y nodejs \
    && rm -rf /var/lib/apt/lists/*

# Copy only project files first to leverage Docker layer caching
COPY ["PatientMgmt/PatientMgmt.csproj", "PatientMgmt/"]
COPY ["Infrastructure/PatientMgmt.Infrastructure.Persistence/PatientMgmt.Infrastructure.Persistence.csproj", "Infrastructure/PatientMgmt.Infrastructure.Persistence/"]
COPY ["Infrastructure/PatientMgmt.Infrastructure.Identity/PatientMgmt.Infrastructure.Identity.csproj", "Infrastructure/PatientMgmt.Infrastructure.Identity/"]
COPY ["Infrastructure/PatientMgmt.Infrastructure.Shared/PatientMgmt.Infrastructure.Shared.csproj", "Infrastructure/PatientMgmt.Infrastructure.Shared/"]
COPY ["Core/PatientMgmt.Core.Application/PatientMgmt.Core.Application.csproj", "Core/PatientMgmt.Core.Application/"]
COPY ["Core/PatientMgmt.Core.Domain/PatientMgmt.Core.Domain.csproj", "Core/PatientMgmt.Core.Domain/"]

# Restore packages for the main project (other project references are present)
RUN dotnet restore "PatientMgmt/PatientMgmt.csproj"

# Copy the rest of the source and publish
COPY . .
WORKDIR "/src/PatientMgmt"
RUN dotnet publish "PatientMgmt.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Expose ports (container) - map to host when running
EXPOSE 8080
EXPOSE 443

# Copy published output
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PatientMgmt.dll"]
