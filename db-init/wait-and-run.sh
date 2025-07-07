#!/bin/bash

set -e

# Vérifie que le mot de passe est défini
if [ -z "$SA_PASSWORD" ]; then
    echo "Erreur : la variable d'environnement SA_PASSWORD n'est pas définie."
    exit 1
fi

echo "Attente complète de SQL Server..."

for i in {1..50}; do
    if /opt/mssql-tools18/bin/sqlcmd -S sqlserver -U sa -P "$SA_PASSWORD" -Q "SELECT name FROM sys.databases WHERE name = 'master'" -N -C > /dev/null 2>&1; then
        echo "SQL Server semble prêt."
        sleep 2  # petit buffer de sécurité
        break
    else
        echo "SQL Server pas encore prêt... ($i)"
        sleep 1
    fi
done

echo "Création de la base MediLaboSolutions..."
/opt/mssql-tools18/bin/sqlcmd -S sqlserver -U sa -P "$SA_PASSWORD" -d master -i ./CreateDatabase.sql -N -C

# Assure-toi que la base est bien dispo avant de continuer
echo "Vérification que la base est bien accessible..."
for i in {1..10}; do
    /opt/mssql-tools18/bin/sqlcmd -S sqlserver -U sa -P "$SA_PASSWORD" -Q "USE MediLaboSolutions; SELECT 1" -N -C > /dev/null 2>&1
    if [ $? -eq 0 ]; then
        echo "Base MediLaboSolutions accessible."
        break
    else
        echo "Attente de disponibilité de la base ($i)..."
        sleep 1
    fi
done

echo "Import des adresses..."
/opt/mssql-tools18/bin/sqlcmd -S sqlserver -U sa -P "$SA_PASSWORD" -d MediLaboSolutions -i ./MigrationAdresses.sql -N -C

echo "Import des patients..."
/opt/mssql-tools18/bin/sqlcmd -S sqlserver -U sa -P "$SA_PASSWORD" -d MediLaboSolutions -i ./MigrationPatients.sql -N -C

echo "Migration terminée."
