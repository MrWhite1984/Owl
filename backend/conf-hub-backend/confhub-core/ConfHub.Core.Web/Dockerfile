# STAGE 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем ВСЁ из ./backend/conf-hub-backend
COPY . .

# Теперь .sln находится здесь: ./conf-hub-backend.sln
RUN dotnet restore "conf-hub-backend.sln"

RUN dotnet publish "confhub-core/ConfHub.Core.Web/ConfHub.Core.Api.csproj" -c Release -o /app/publish

# STAGE 2: final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app
RUN adduser --disabled-password --gecos '' appuser

COPY --from=build /app/publish .
RUN chown -R appuser /app

USER appuser

EXPOSE 8080
ENTRYPOINT ["dotnet", "ConfHub.Core.Api.dll"]