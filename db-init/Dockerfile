FROM mcr.microsoft.com/mssql/server:2022-latest

COPY ./db-init/wait-and-run.sh /wait-and-run.sh
COPY ./db-init/MigrationAdresses.sql /MigrationAdresses.sql
COPY ./db-init/MigrationPatients.sql /MigrationPatients.sql

CMD ["/wait-and-run.sh"]
