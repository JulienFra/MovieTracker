# 🍿 ExamMovieTracker

![Blazor](https://img.shields.io/badge/Blazor-Server-512BD4?style=for-the-badge&logo=blazor&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-8.0_/_9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Tests](https://img.shields.io/badge/Tests-xUnit-4CAF50?style=for-the-badge)

**ExamMovieTracker** est une application Web développée en ASP.NET Core Blazor. 
Ce projet a été réalisé dans le cadre de l'examen de **Programmation Orientée Objet** dispensé par Jérémy Kairis.

---

## ✨ Fonctionnalités (Consignes de l'examen)

L'application respecte l'entièreté du cahier des charges demandé :

* 🌍 **API Externe** : Affichage des données (films populaires, recherche) venant de l'API TheMovieDatabase (TMDB).
* 🔐 **Authentification** : Système de connexion (Identity) requis pour gérer ses propres listes.
* 💾 **Favoris Locaux** : Création de favoris sauvegardés localement (via le LocalStorage du navigateur) pour chaque utilisateur.
* 📝 **Modification des données** : L'utilisateur peut ajouter des commentaires personnels sur ses films favoris, modifiant ainsi l'état de la donnée en local.
* 🗑️ **Gestion** : Possibilité de gérer et supprimer ses favoris à tout moment.
* 🧪 **Tests** : Le projet est accompagné d'une suite de tests unitaires (xUnit) validant la logique métier (modèles).

---

## 🛠️ Prérequis

* SDK .NET (version 8.0 ou 9.0)
* Une clé API valide de [TMDB (TheMovieDatabase)](https://developer.themoviedb.org/docs)

---

## 🚀 Installation et Lancement

**1. Cloner le projet**
```bash
git clone [https://github.com/JulienFra/ExamMovieTracker.git](https://github.com/JulienFra/ExamMovieTracker.git)
cd ExamMovieTracker
```
**2. Configurer la clé API**
Dans le dossier du projet principal (ExamMovieTracker), ouvre le fichier appsettings.json et insère ta clé API TMDB :
```json
{
  "TmdbApiKey": "TA_CLE_API_ICI",
  "AllowedHosts": "*"
}
```

**3. Préparer la base de données (Authentification)**
Dans ton terminal, lance la commande suivante pour créer la base de données locale des utilisateurs :
```bash
dotnet ef database update
```

**4. Lancer l'application**
```bash
dotnet run
```
L'application sera accessible dans ton navigateur à l'adresse indiquée dans le terminal (généralement https://localhost:5001).

---

## 🧪 Exécuter les tests

Le projet inclut des tests unitaires validant le bon fonctionnement de la classe Movie et l'enregistrement des commentaires locaux.

Pour les exécuter, ouvre un terminal à la racine de la solution et tape :
```bash
dotnet test
```