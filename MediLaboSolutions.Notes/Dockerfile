# Image de base pour exécution
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Étape de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copie ciblée des projets nécessaires
COPY ["MediLaboSolutions.Notes/MediLaboSolutions.Notes.csproj", "MediLaboSolutions.Notes/"]
COPY ["MediLaboSolutions.Common/MediLaboSolutions.Common.csproj", "MediLaboSolutions.Common/"]

RUN dotnet restore "MediLaboSolutions.Notes/MediLaboSolutions.Notes.csproj"

# Copie ciblée du code source
COPY MediLaboSolutions.Notes/ MediLaboSolutions.Notes/
COPY MediLaboSolutions.Common/ MediLaboSolutions.Common/

WORKDIR "/src/MediLaboSolutions.Notes"
RUN dotnet build "MediLaboSolutions.Notes.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

# Étape de publication
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MediLaboSolutions.Notes.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

# Image finale (production) sécurisée
FROM base AS final
WORKDIR /app

# Ajout d'un utilisateur non-root
RUN adduser --disabled-password --gecos "" appuser
USER appuser

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MediLaboSolutions.Notes.dll"]