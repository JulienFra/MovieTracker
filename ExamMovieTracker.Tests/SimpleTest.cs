using Bunit;
using Xunit;
// Remplace par le bon dossier où se trouve tes pages/composants
using ExamMovieTracker.Components.Pages;
using ExamMovieTracker.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Security.Claims;

namespace ExamMovieTrackertest
{
    // La classe hérite de BunitContext, ce qui nous donne accès aux outils pour générer des composants Blazor
    public class ComposantSimpleTests : BunitContext
    {
        // Le constructeur s'exécute avant chaque test. C'est ici qu'on prépare "l'environnement" (le Arrange).
        public ComposantSimpleTests()
        {
            // 1. Simule le LocalStorage pour éviter un crash si la page Home essaie de lire/écrire des données
            Services.AddBlazoredLocalStorage();

            // 2. Injecte notre faux fournisseur d'authentification (défini tout en bas de ce fichier)
            Services.AddScoped<AuthenticationStateProvider, TestAuthenticationStateProvider>();

            // 3. Simule la configuration (le fichier appsettings.json) avec une fausse clé API 
            // Cela permet au test de fonctionner sans exposer ta vraie clé TMDB
            var inMemorySettings = new Dictionary<string, string>
            {
                { "TmdbApiKey", "fake-test-api-key" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            Services.AddSingleton(configuration);

            // 4. Enregistre un outil pour faire des requêtes réseau (requis par TmdbService)
            Services.AddScoped<HttpClient>();

            // 5. Enregistre le service TMDB pour que l'injection de dépendances (@inject) fonctionne dans la page Home
            Services.AddScoped<TmdbService>();
        }

        // L'attribut [Fact] indique à xUnit que cette méthode est un test à exécuter
        [Fact]
        public void Test_Page_Accueil_Affiche_Titre()
        {
            // --- ACT (L'action) ---
            // On demande à bUnit de générer la page d'accueil (Home). 
            // Il va utiliser tous les faux services configurés dans le constructeur juste au-dessus.
            var cut = Render<Home>();

            // --- ASSERT (La vérification) ---
            // On inspecte le code HTML généré (cut.Markup) pour vérifier si le mot "MovieTracker" s'y trouve bien.
            Assert.Contains("MovieTracker", cut.Markup);
        }

        // =====================================================================
        // CLASSE UTILE : Fournit un faux état d'authentification pour le test
        // =====================================================================
        private class TestAuthenticationStateProvider : AuthenticationStateProvider
        {
            public override Task<AuthenticationState> GetAuthenticationStateAsync()
            {
                // On crée une ClaimsIdentity VÉRIFIQUEMENT VIDE. 
                // Pour Blazor, une identité vide signifie "l'utilisateur n'est pas connecté" (visiteur anonyme).
                var identity = new ClaimsIdentity(); 
                var user = new ClaimsPrincipal(identity);
                
                // On retourne cet état au système
                return Task.FromResult(new AuthenticationState(user));
            }
        }
        
    }
}