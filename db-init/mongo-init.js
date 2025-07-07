// Script d'initialisation MongoDB
// Ce script s'exécute automatiquement au premier démarrage uniquement
// si la base de données MediLaboSolutions n'existe pas encore

print('=== Début de l\'initialisation MongoDB ===');

// Sélection de la base de données
db = db.getSiblingDB('MediLaboSolutions');

try {
    // Vérification si la collection Notes existe déjà et contient des données
    const notesCount = db.Notes.countDocuments();

    if (notesCount > 0) {
        print(`Collection Notes existe déjà avec ${notesCount} documents. Initialisation ignorée.`);
    } else {
        print('Collection Notes vide ou inexistante. Chargement des données initiales...');

        // Données des notes à insérer (intégrées directement dans le script)
        const notes = [
            {
                "_id": ObjectId("681a060bf963a1cc416684bc"),
                "DateCreation": new Date("2025-05-06T12:52:02.633Z"),
                "Nom": "TestNone",
                "PatientId": 2,
                "PraticienUsername": "Dr.Medecin@medilabo.fr",
                "Prenom": "Test",
                "Texte": "Le patient déclare qu'il 'se sent très bien' Poids égal ou inférieur au poids recommandé"
            },
            {
                "_id": ObjectId("681a0622f963a1cc416684bd"),
                "DateCreation": new Date("2025-05-06T12:52:02.633Z"),
                "Nom": "TestBorderline",
                "PatientId": 3,
                "PraticienUsername": "Dr.Medecin@medilabo.fr",
                "Prenom": "Test",
                "Texte": "Le patient déclare qu'il ressent beaucoup de stress au travail Il se plaint également que son audition est anormale dernièrement"
            },
            {
                "_id": ObjectId("681a062bf963a1cc416684be"),
                "DateCreation": new Date("2025-05-06T12:52:02.633Z"),
                "Nom": "TestBorderline",
                "PatientId": 3,
                "PraticienUsername": "Dr.Medecin@medilabo.fr",
                "Prenom": "Test",
                "Texte": "Le patient déclare avoir fait une réaction aux médicaments au cours des 3 derniers mois Il remarque également que son audition continue d'être anormale"
            },
            {
                "_id": ObjectId("681a063ef963a1cc416684bf"),
                "DateCreation": new Date("2025-05-06T12:52:02.633Z"),
                "Nom": "TestInDanger",
                "PatientId": 4,
                "PraticienUsername": "Dr.Medecin@medilabo.fr",
                "Prenom": "Test",
                "Texte": "Le patient déclare qu'il fume depuis peu"
            },
            {
                "_id": ObjectId("681a064af963a1cc416684c0"),
                "DateCreation": new Date("2025-05-06T12:52:02.633Z"),
                "Nom": "TestInDanger",
                "PatientId": 4,
                "PraticienUsername": "Dr.Medecin@medilabo.fr",
                "Prenom": "Test",
                "Texte": "Le patient déclare qu'il est fumeur et qu'il a cessé de fumer l'année dernière Il se plaint également de crises d'apnée respiratoire anormales Tests de laboratoire indiquant un taux de cholestérol LDL élevé"
            },
            {
                "_id": ObjectId("681a0676f963a1cc416684c1"),
                "DateCreation": new Date("2025-05-06T12:52:02.633Z"),
                "Nom": "TestEarlyOnset",
                "PatientId": 5,
                "PraticienUsername": "Dr.Medecin@medilabo.fr",
                "Prenom": "Test",
                "Texte": "Le patient déclare qu'il lui est devenu difficile de monter les escaliers Il se plaint également d'être essoufflé Tests de laboratoire indiquant que les anticorps sont élevés Réaction aux médicaments"
            },
            {
                "_id": ObjectId("681a067bf963a1cc416684c2"),
                "DateCreation": new Date("2025-05-06T12:52:02.633Z"),
                "Nom": "TestEarlyOnset",
                "PatientId": 5,
                "PraticienUsername": "Dr.Medecin@medilabo.fr",
                "Prenom": "Test",
                "Texte": "Le patient déclare qu'il a mal au dos lorsqu'il reste assis pendant longtemps"
            },
            {
                "_id": ObjectId("681a0682f963a1cc416684c3"),
                "DateCreation": new Date("2025-05-06T12:52:02.633Z"),
                "Nom": "TestEarlyOnset",
                "PatientId": 5,
                "PraticienUsername": "Dr.Medecin@medilabo.fr",
                "Prenom": "Test",
                "Texte": "Le patient déclare avoir commencé à fumer depuis peu Hémoglobine A1C supérieure au niveau recommandé"
            },
            {
                "_id": ObjectId("681a0689f963a1cc416684c4"),
                "DateCreation": new Date("2025-05-06T12:52:02.633Z"),
                "Nom": "TestEarlyOnset",
                "PatientId": 5,
                "PraticienUsername": "Dr.Medecin@medilabo.fr",
                "Prenom": "Test",
                "Texte": "Taille, Poids, Cholestérol, Vertige et Réaction"
            },
            {
                "_id": ObjectId("68344afff43b5e77a5cfbbbb"),
                "DateCreation": new Date("2025-05-26T11:05:35.662Z"),
                "PatientId": 24,
                "PraticienUsername": "Dr.Medecin@medilabo.fr",
                "Texte": "Hémoglobine A1C ;\n● Microalbumine ;\n● Fumeur taille"
            },
            {
                "_id": ObjectId("68344b53f43b5e77a5cfbbbc"),
                "DateCreation": new Date("2025-05-26T11:06:59.019Z"),
                "PatientId": 24,
                "PraticienUsername": "Dr.Medecin@medilabo.fr",
                "Texte": "poids"
            },
            {
                "_id": ObjectId("68344b88f43b5e77a5cfbbbd"),
                "DateCreation": new Date("2025-05-26T11:07:52.806Z"),
                "PatientId": 24,
                "PraticienUsername": "Dr.Medecin@medilabo.fr",
                "Texte": "Fumeur Fumeur Fumeur Fumeur Fumeur Fumeur Fumeur "
            },
            {
                "_id": ObjectId("68344ba6f43b5e77a5cfbbbe"),
                "DateCreation": new Date("2025-05-26T11:08:22.147Z"),
                "PatientId": 24,
                "PraticienUsername": "Dr.Medecin@medilabo.fr",
                "Texte": "fumer fumeuse"
            }
        ];

        // Insertion des données (déjà formatées correctement)
        const result = db.Notes.insertMany(notes);

        print(`✅ ${result.insertedIds.length} notes insérées avec succès dans la collection Notes`);

        // Création d'index pour optimiser les performances
        db.Notes.createIndex({ "PatientId": 1 });
        db.Notes.createIndex({ "PraticienUsername": 1 });
        db.Notes.createIndex({ "DateCreation": -1 });

        print('✅ Index créés sur PatientId, PraticienUsername et DateCreation');
    }

    print('=== Initialisation MongoDB terminée avec succès ===');

} catch (error) {
    print('❌ Erreur lors de l\'initialisation MongoDB:');
    print(error);
    throw error;
}