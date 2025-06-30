# Runtime de base
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Étape de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copie ciblée des fichiers projet
COPY ["MediLaboSolutions.Gateway/MediLaboSolutions.Gateway.csproj", "MediLaboSolutions.Gateway/"]

RUN dotnet restore "MediLaboSolutions.Gateway/MediLaboSolutions.Gateway.csproj"

# Copie ciblée des sources
COPY MediLaboSolutions.Gateway/ MediLaboSolutions.Gateway/

WORKDIR "/src/MediLaboSolutions.Gateway"
RUN dotnet build "MediLaboSolutions.Gateway.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

# Étape de publication
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MediLaboSolutions.Gateway.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

# Image finale sécurisée
FROM base AS final
WORKDIR /app

# Création d’un utilisateur non-root
RUN adduser --disabled-password --gecos "" appuser
USER appuser

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MediLaboSolutions.Gateway.dll"]
