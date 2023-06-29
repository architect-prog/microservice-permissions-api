FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Microservice.Permissions.Api/Microservice.Permissions.Api.csproj", "Microservice.Permissions.Api/"]
COPY ["Microservice.Permissions.Core/Microservice.Permissions.Core.csproj", "Microservice.Permissions.Core/"]
COPY ["Microservice.Permissions.Kernel/Microservice.Permissions.Kernel.csproj", "Microservice.Permissions.Kernel/"]
COPY ["Microservice.Permissions.Persistence.EfCore/Microservice.Permissions.Persistence.EfCore.csproj", "Microservice.Permissions.Persistence.EfCore/"]
COPY ["Microservice.Permissions.Persistence.Dapper/Microservice.Permissions.Persistence.Dapper.csproj", "Microservice.Permissions.Persistence.Dapper/"]
COPY ["Microservice.Permissions.Messaging/Microservice.Permissions.Messaging.csproj", "Microservice.Permissions.Messaging/"]
RUN dotnet restore "Microservice.Permissions.Api/Microservice.Permissions.Api.csproj"
COPY . .
WORKDIR "/src/Microservice.Permissions.Api"
RUN dotnet build "Microservice.Permissions.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Permissions.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Permissions.Api.dll"]
