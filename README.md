# Guide d'installation et de lancement

## Clonage du dépôt

```bash
git clone <url-du-repo>
cd P10.MediLaboSolutions
```

## Préparation de l'environnement

Copiez les fichiers d'exemple nécessaires :

```bash
cp .env.example .env
cp MediLaboSolutions.API/appsettings_example.json MediLaboSolutions.API/appsettings.json
cp MediLaboSolutions.Evaluation/appsettings_example.json MediLaboSolutions.Evaluation/appsettings.json
cp MediLaboSolutions.Gateway/appsettings_example.json MediLaboSolutions.Gateway/appsettings.json
cp MediLaboSolutions.Notes/appsettings_example.json MediLaboSolutions.Notes/appsettings.json
cp MediLaboSolutions.Web/appsettings_example.json MediLaboSolutions.Web/appsettings.json
```

## Construction et lancement

Construisez les images puis démarrez les services :

**REMARQUE :** Assuez-vous que le script wait-and-run.sh soit encodé en LF et non CRLF. Le script ne fonctionnera pas avec l'encodage CRLF.

```bash
docker-compose build --no-cache
docker-compose up -d
```

## Ports des services

- Passerelle : `8080`
- API : `8081`
- Evaluation : `8082`
- Notes : `8083`
- Interface Web : `5000`
- MongoDB : `27017`
- SQL Server : `1433`

## Création du compte et migrations

1. Lancez l'application web via l'URL http://localhost:5000.
2. Inscrivez-vous à l'aide du formulaire d'inscription.
3. Appliquez les migrations de base de données si nécessaire en cliquant sur le bouton **Apply Migration**.
4. Actualisez la page puis confirmez votre compte.
5. Connectez-vous pour accéder au site.

## Connexion à la base de données

Pour utiliser SQL Server Management Studio, récupérez la chaîne de connexion dans le fichier `.env` et utilisez-la pour vous connecter au serveur SQL.

