## Étape 6 – Green Code : Enjeux et recommandations

Ce projet .NET 9 avec Docker étant finalisé sur le plan fonctionnel et technique, cette étape vise à identifier les enjeux liés au Green Code et à proposer des pistes concrètes d’amélioration écologique.

---

### 1. Objectif du Green Code

Le Green Code vise à réduire l’empreinte écologique des logiciels, principalement en limitant leur consommation de ressources (CPU, mémoire, réseau, stockage) à l’exécution.  
Contrairement à l’optimisation classique (centrée sur la performance), il s’agit ici d’améliorer **l’efficience énergétique**.

---

### 2. Enjeux et axes d'amélioration

Trois axes permettent de limiter l’impact environnemental d’une application :

- **Frugalité fonctionnelle** : supprimer ou éviter les fonctionnalités inutiles ou rarement utilisées.
- **Optimisation du contenant** : améliorer la structure technique (code, infrastructure, appels serveurs, stockage).
- **Optimisation du contenu** : alléger les données échangées (images, vidéos, scripts, documents), réduire les appels réseau.

---

### 3. Analyse du projet

#### Points positifs

- Architecture microservices claire, avec séparation des responsabilités.
- Pas de script ou asset inutile chargé côté frontend.
- Réutilisation de certaines classes et projet Common permettant d'éviter la duplication de code
- Utilisation de conteneurs Docker facilitant l’allocation raisonnée des ressources.
- Utilisation de Sonar pour l'analyse qualité du code

#### Pistes d’amélioration

| Composant             | Observation                                                       | Piste d’amélioration                                  |
|-----------------------|-------------------------------------------------------------------|--------------------------------------------------------|
| API / Gateway         | Multiplication d’appels internes (rechargement de page, etc)      | Implémenter un cache local par exemple                  |
| Frontend Web          | Il n'y a pas de système de pagination en place pour le moment     | Ajouter de la pagination, limiter les détails initiaux |
| Fichiers statiques    | Pas d’optimisation spécifique sur les images ou assets           | Conversion en WebP, compression, lazy loading          |
| Analyse Sonar Qube    | Pas de plugin spécifique pour l'analyse du green code            | Ajout de plugin comme *Green Code Initiative*          |

---

### 4. Recommandations générales

- Identifier les parcours utilisateurs clés pour concentrer les optimisations.
- Réduire les appels réseau en cache ou différant les requêtes secondaires.
- Éviter les traitements inutiles dans les composants critiques (API, front).
- Préférer les structures de données sobres et efficaces en mémoire.
- Mettre en place un cache dans les cas où il s'avèrerait utile.
- Mesurer l’impact écologique réel avec des outils comme EcoIndex ou Ecometer.

---

### 5. Références

- Cours OpenClassrooms – *Réduisez l’empreinte écologique de votre site web*
- INR – *Green code : écrivez du code vert !*
- Scitepress – *How Green Are Java Best Coding Practices?*
- code.gouv.fr - *La route vers le « green code » (avec Sonar)*
- [Gree Code Initiative](https://green-code-initiative.org/)

---
