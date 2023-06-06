FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ItemsService/ItemsService.csproj", "ItemsService/"]
RUN dotnet restore "ItemsService/ItemsService.csproj"
COPY . .
WORKDIR "/src/ItemsService"
RUN dotnet build "ItemsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ItemsService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ../OLXFakedBackend/DeploymentData ./DeploymentData
# Copy your .pfx file to the Docker container
COPY ../certs/aspnetapp.pfx /https/

# Set necessary environment variables
ENV ASPNETCORE_URLS="https://+;http://+"
ENV ASPNETCORE_HTTPS_PORT=5001
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="{ password here }"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/https/aspnetapp.pfx"

ENTRYPOINT ["dotnet", "ItemsService.dll"]
