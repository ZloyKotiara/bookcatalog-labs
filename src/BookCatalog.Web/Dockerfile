FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/BookCatalog.Web/BookCatalog.Web.csproj", "src/BookCatalog.Web/"]
RUN dotnet restore "src/BookCatalog.Web/BookCatalog.Web.csproj"

COPY . .
WORKDIR "/src/src/BookCatalog.Web"
RUN dotnet publish "BookCatalog.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
RUN apt-get update     && apt-get install -y curl     && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

HEALTHCHECK --interval=30s --timeout=5s --start-period=20s --retries=3   CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "BookCatalog.Web.dll"]
