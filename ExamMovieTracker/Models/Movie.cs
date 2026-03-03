using System.Text.Json.Serialization;

namespace ExamMovieTracker.Models;

public class Movie
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("overview")]
    public string Overview { get; set; } = string.Empty;

    [JsonPropertyName("poster_path")]
    public string PosterPath { get; set; } = string.Empty;

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; set; }
}

// Cette classe sert à lire la réponse de l'API qui met les films dans un tableau "results"
public class TmdbResponse
{
    [JsonPropertyName("results")]
    public List<Movie> Results { get; set; } = new();
}