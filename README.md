# TaskManagerAPI

Une API REST développée en .NET Core pour gérer des tâches. 
Projet de remise à niveau intégrant de bonnes pratiques modernes : logging, sécurité, documentation, architecture propre, etc.

## 🛠 Stack utilisée

- ASP.NET Core
- Entity Framework Core + IdentityDbContext
- Authentification JWT
- Swagger
- Serilog + Seq pour les logs
- AutoMapper
- Injection de dépendances
- Middleware pour la gestion des erreurs
- Result pattern

## 🚀 Lancer le projet localement

### Prérequis

- [.NET SDK 6.0+](https://dotnet.microsoft.com/download)
- SQL Server local (ou modifie la chaîne de connexion)
- [Seq](https://datalust.co/seq) (optionnel pour les logs)

### Étapes

1. Cloner le projet :

```bash
git clone https://github.com/ton-utilisateur/task-manager-api.git
cd task-manager-api