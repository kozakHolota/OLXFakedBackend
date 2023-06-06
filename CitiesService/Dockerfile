FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CitiesService/CitiesService.csproj", "CitiesService/"]
RUN dotnet restore "CitiesService/CitiesService.csproj"
COPY . .

WORKDIR "/src/CitiesService"
RUN dotnet build "CitiesService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CitiesService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ../OLXFakedBackend/DeploymentData ./DeploymentData

ENTRYPOINT ["dotnet", "CitiesService.dll"]
