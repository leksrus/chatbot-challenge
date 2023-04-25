FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV TZ America/Argentina/Buenos_Aires

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /sln

COPY *.sln .

COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done

RUN dotnet restore --no-cache

COPY . .
WORKDIR "/sln/src/StockConsumerSub"
RUN dotnet publish -c Release -o /app


FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "StockConsumerSub.dll"]
