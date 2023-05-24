FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EO.IdentityServer/EO.IdentityServer.csproj", "EO.IdentityServer/"]
RUN dotnet restore "EO.IdentityServer/EO.IdentityServer.csproj"
COPY . .
WORKDIR "/src/EO.IdentityServer"
RUN dotnet build "EO.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EO.IdentityServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EO.IdentityServer.dll"]