FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/NodeInfo.App/NodeInfo.App.csproj", "src/NodeInfo.App/"]
RUN dotnet restore "src/NodeInfo.App/NodeInfo.App.csproj"

COPY . .
WORKDIR "/src/src/NodeInfo.App"
RUN dotnet publish "NodeInfo.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "NodeInfo.App.dll"]
