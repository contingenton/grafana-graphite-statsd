FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build

RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6-dev \
        libgdiplus \
        libx11-dev \
     && rm -rf /var/lib/apt/lists/*

WORKDIR /app

# Install production dependencies.
# Copy csproj and restore as distinct layers.

COPY ./StatsWeb.Api.csproj ./StatsWeb.Api/
RUN dotnet restore ./StatsWeb.Api/StatsWeb.Api.csproj

COPY ./ ./StatsWeb.Api/

WORKDIR /app/StatsWeb.Api
RUN dotnet build -c Release --no-restore

FROM build AS publish
WORKDIR /app/StatsWeb.Api
RUN dotnet publish -c Release -o /app/out --no-build

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6-dev \
        libgdiplus \
        libx11-dev \
     && rm -rf /var/lib/apt/lists/*
     
WORKDIR /app
COPY --from=publish /app/out ./

ENTRYPOINT ["dotnet", "StatsWeb.Api.dll"]
