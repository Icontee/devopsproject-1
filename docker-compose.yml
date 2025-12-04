# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["DevOpsDemo.csproj", "./"]
RUN dotnet restore "DevOpsDemo.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "DevOpsDemo.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "DevOpsDemo.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy published app
COPY --from=publish /app/publish .

# Set environment variable
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "DevOpsDemo.dll"]
