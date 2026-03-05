using System.Net.Http.Json;
using ExamMovieTracker.Models;
using Microsoft.Extensions.Configuration; 

namespace ExamMovieTracker.Services;

// === SERVICE DE COMMUNICATION AVEC L'API TMDB ===
// Cette classe centralise tous les appels réseau vers l'API TheMovieDatabase.
// Elle est injectée (Dependency Injection) dans nos pages Blazor via @inject TmdbService.
public class TmdbService
{
    // Variables privées en lecture seule (assignées une seule fois dans le constructeur)
    private readonly HttpClient _httpClient; // L'outil qui permet de faire des requêtes HTTP (GET, POST...)
    private readonly string _apiKey;         // Stocke la clé secrète de l'API
    
    // Constante pour l'URL de base, ce qui évite de la retaper à chaque requête
    private const string BaseUrl = "https://api.themoviedb.org/3";

    // === CONSTRUCTEUR ===
    // IConfiguration permet d'aller lire le fichier appsettings.json de manière sécurisée.
    // Cela évite d'écrire la clé API "en dur" dans le code, ce qui est une mauvaise pratique de sécurité.
    public TmdbService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        
        // On récupère la clé "TmdbApiKey" depuis appsettings.json.
        // L'opérateur '??' lève une exception claire si on a oublié de configurer la clé.
        _apiKey = config["TmdbApiKey"] ?? throw new Exception("Clé API introuvable dans appsettings.json !");
    }

    // === MÉTHODE 1 : RÉCUPÉRER LES FILMS POPULAIRES ===
    // Méthode asynchrone (Task) qui renvoie une liste de films. Paramètre 'page' par défaut à 1.
    public async Task<List<Movie>> GetPopularMoviesAsync(int page = 1)
    {
        // Construction de l'URL avec interpolation de chaînes ($"...")
        var url = $"{BaseUrl}/movie/popular?api_key={_apiKey}&language=fr-FR&page={page}";
        
        // On fait un appel GET et on désérialise automatiquement le JSON en objet TmdbResponse
        var response = await _httpClient.GetFromJsonAsync<TmdbResponse>(url);
        
        // Si response est null, on renvoie une liste vide au lieu de faire crasher l'application
        return response?.Results ?? new List<Movie>();
    }

    // === MÉTHODE 2 : RECHERCHER UN FILM PAR NOM ===
    public async Task<List<Movie>> SearchMoviesAsync(string query)
    {
        // Sécurité : Si l'utilisateur a tapé une recherche vide ou juste des espaces, on arrête tout de suite
        if (string.IsNullOrWhiteSpace(query)) return new List<Movie>();

        // Uri.EscapeDataString(query) est très important ! 
        // Si l'utilisateur tape "Spider Man" (avec un espace), cela le transforme en "Spider%20Man" 
        // pour que l'URL reste valide et ne plante pas.
        var url = $"{BaseUrl}/search/movie?api_key={_apiKey}&language=en-US&query={Uri.EscapeDataString(query)}&page=1&include_adult=false";
        
        var response = await _httpClient.GetFromJsonAsync<TmdbResponse>(url);
        return response?.Results ?? new List<Movie>();
    }

    // === MÉTHODE 3 : RÉCUPÉRER LES DÉTAILS D'UN SEUL FILM ===
    // Renvoie un "Movie?" (nullable) car le film pourrait ne pas exister.
    public async Task<Movie?> GetMovieDetailsAsync(int movieId)
    {
        try
        {
            var url = $"{BaseUrl}/movie/{movieId}?api_key={_apiKey}&language=en-US";
            
            // Ici on désérialise directement en "Movie" (et non TmdbResponse) car l'API 
            // renvoie directement les infos du film quand on cherche par ID, sans tableau "results".
            return await _httpClient.GetFromJsonAsync<Movie>(url);
        }
        catch
        {
            // Si l'API renvoie une erreur (ex: erreur 404 si l'ID n'existe pas),
            // on attrape l'erreur et on renvoie null pour que notre page Blazor affiche "Film non trouvé".
            return null;
        }
    }
}