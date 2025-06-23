#!/bin/bash

echo "⏳ Attente de MongoDB..."
until mongo --host mongodb --eval "db.stats()"; do
  sleep 1
done

echo "🔍 Vérification si la collection existe déjà..."
COL_EXISTS=$(mongo --quiet --host mongodb --eval "db.getMongo().getDB('MediLaboSolutions').getCollectionNames().indexOf('Notes') >= 0")

if [ "$COL_EXISTS" == "true" ]; then
  echo "✅ Données déjà présentes, aucune action nécessaire."
else
  echo "🚀 Import initial des données..."
  mongoimport \
    --host mongodb \
    --db MediLaboSolutions \
    --collection Notes \
    --file /data/MediLaboSolutions.Notes.json \
    --jsonArray
  echo "✅ Import terminé."
fi
