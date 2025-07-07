# NOSONAR: Image SQL Server nécessite root
FROM mcr.microsoft.com/mssql/server:2022-latest

WORKDIR /tmp/db-seed

ENV SA_PASSWORD=YourStrong@Passw0rd

COPY ./db-init/wait-and-run.sh ./wait-and-run.sh
COPY ./db-init/CreateDatabase.sql ./CreateDatabase.sql
COPY ./db-init/MigrationAdresses.sql ./MigrationAdresses.sql
COPY ./db-init/MigrationPatients.sql ./MigrationPatients.sql

# Pas besoin de chmod

# On appelle bash explicitement
CMD ["bash", "./wait-and-run.sh"]
