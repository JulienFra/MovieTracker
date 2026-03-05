using System.Text.Json.Serialization;

namespace ExamMovieTracker.Models;

// === MODÈLE DE DONNÉES : FILM ===
// Cette classe représente un film exact tel qu'il est modélisé par l'API TMDB.
public class Movie
{
    // L'attribut [JsonPropertyName] est crucial : il indique à notre programme C# 
    // comment faire correspondre le nom exact de la clé dans le JSON brut ("id") 
    // avec le nom de notre propriété en C# ("Id" avec une majuscule).
    
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("overview")]
    public string Overview { get; set; } = string.Empty;

    // L'API ne renvoie que la fin de l'URL de l'image (ex: "/vUv1oNpw....jpg").
    // Il faudra concaténer l'URL de base (https://image.tmdb.org/t/p/w500) devant pour l'afficher dans les vues.
    [JsonPropertyName("poster_path")]
    public string PosterPath { get; set; } = string.Empty;

    // La note moyenne du film (un nombre à virgule, d'où le type 'double')
    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; set; }
}

// === CLASSE DE RÉPONSE API ===
// L'API TMDB ne renvoie pas directement une liste de films [ {}, {} ], 
// mais un objet JSON global contenant des métadonnées (page, total_pages) 
// et un tableau nommé "results" qui contient nos films.
// Cette classe sert de "réceptacle" ou "d'entonnoir" pour lire spécifiquement ce tableau.
public class TmdbResponse
{
    [JsonPropertyName("results")]
    public List<Movie> Results { get; set; } = new(); // Initialise une liste vide par défaut pour éviter les erreurs nulles (NullReferenceException)
}