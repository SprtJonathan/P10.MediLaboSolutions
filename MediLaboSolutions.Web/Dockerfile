# Runtime de base (exécution uniquement)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Étape de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copie ciblée des .csproj nécessaires
COPY ["MediLaboSolutions.Web/MediLaboSolutions.Web.csproj", "MediLaboSolutions.Web/"]
COPY ["MediLaboSolutions.Common/MediLaboSolutions.Common.csproj", "MediLaboSolutions.Common/"]

RUN dotnet restore "MediLaboSolutions.Web/MediLaboSolutions.Web.csproj"

# Copie ciblée des sources
COPY MediLaboSolutions.Web/ MediLaboSolutions.Web/
COPY MediLaboSolutions.Common/ MediLaboSolutions.Common/

WORKDIR "/src/MediLaboSolutions.Web"
RUN dotnet build "MediLaboSolutions.Web.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

# Étape de publication
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MediLaboSolutions.Web.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

# Image finale sécurisée (non-root)
FROM base AS final
WORKDIR /app

# Création d’un utilisateur non-root
RUN adduser --disabled-password --gecos "" appuser
USER appuser

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MediLaboSolutions.Web.dll"]