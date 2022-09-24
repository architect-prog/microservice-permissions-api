FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservice.Permisssions.Api/Microservice.Permisssions.Api.csproj", "Microservice.Permisssions.Api/"]
RUN dotnet restore "Microservice.Permisssions.Api/Microservice.Permisssions.Api.csproj"
COPY . .
WORKDIR "/src/Microservice.Permisssions.Api"
RUN dotnet build "Microservice.Permisssions.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Permisssions.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Permisssions.Api.dll"]
