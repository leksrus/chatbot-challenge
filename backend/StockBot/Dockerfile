FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Zona horaria
ENV TZ America/Argentina/Buenos_Aires

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /sln

# Copia el archivo de solucion .sln
COPY *.sln .

# Copia todos los proyectos .csproj de la carpeta src/
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done

# Copia todos los proyectos de test de la carpeta tests/
COPY tests/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p tests/${file%.*}/ && mv $file tests/${file%.*}/; done

RUN dotnet nuget add source http://dmz-test-nexus.gsdmz.net.ar:8081/repository/nuget-group/index.json -n "dmztest-nugetgroup"
RUN dotnet restore --no-cache

COPY . .
WORKDIR "/sln/src/StockBot.API"
RUN dotnet publish -c Release -o /app

# Run unit tests
# FROM build as testing
# WORKDIR /sln/tests/StockBot.UnitTests
# RUN dotnet test

# Run integration tests
# FROM build as integration-testing
# WORKDIR /sln/tests/StockBot.IntegrationTests
# RUN dotnet test

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "StockBot.API.dll"]