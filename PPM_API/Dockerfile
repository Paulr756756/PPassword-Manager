FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PPM_API/PPM_API.csproj", "PPM_API/"]
RUN dotnet restore "PPM_API/PPM_API.csproj"
COPY . .
WORKDIR "/src/PPM_API"
RUN dotnet build "PPM_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PPM_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PPM_API.dll"]
