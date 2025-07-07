# Base runtime image avec user 'app'
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copie ciblée des .csproj
COPY ["MediLaboSolutions.Evaluation/MediLaboSolutions.Evaluation.csproj", "MediLaboSolutions.Evaluation/"]
COPY ["MediLaboSolutions.Common/MediLaboSolutions.Common.csproj", "MediLaboSolutions.Common/"]

RUN dotnet restore "MediLaboSolutions.Evaluation/MediLaboSolutions.Evaluation.csproj"

# Copie ciblée des sources uniquement nécessaires
COPY MediLaboSolutions.Evaluation/ MediLaboSolutions.Evaluation/
COPY MediLaboSolutions.Common/ MediLaboSolutions.Common/

WORKDIR "/src/MediLaboSolutions.Evaluation"
RUN dotnet build "MediLaboSolutions.Evaluation.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MediLaboSolutions.Evaluation.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

# Final stage avec user non-root
FROM base AS final
WORKDIR /app

# Création utilisateur non-root sécurisé
RUN adduser --disabled-password --gecos "" appuser
USER appuser

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MediLaboSolutions.Evaluation.dll"]