using System.Net.Http.Json;
using ExamMovieTracker.Models;
using Microsoft.Extensions.Configuration; 

namespace ExamMovieTracker.Services;

public class TmdbService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private const string BaseUrl = "https://api.themoviedb.org/3";

    public TmdbService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["TmdbApiKey"] ?? throw new Exception("Clé API introuvable !");
    }

    public async Task<List<Movie>> GetPopularMoviesAsync(int page = 1)
    {
        var url = $"{BaseUrl}/movie/popular?api_key={_apiKey}&language=fr-FR&page={page}";
        
        var response = await _httpClient.GetFromJsonAsync<TmdbResponse>(url);
        return response?.Results ?? new List<Movie>();
    }

    public async Task<List<Movie>> SearchMoviesAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return new List<Movie>();

        var url = $"{BaseUrl}/search/movie?api_key={_apiKey}&language=en-US&query={Uri.EscapeDataString(query)}&page=1&include_adult=false";
        
        var response = await _httpClient.GetFromJsonAsync<TmdbResponse>(url);
        return response?.Results ?? new List<Movie>();
    }
    public async Task<Movie?> GetMovieDetailsAsync(int movieId)
    {
        try
        {
            var url = $"{BaseUrl}/movie/{movieId}?api_key={_apiKey}&language=en-US";
            return await _httpClient.GetFromJsonAsync<Movie>(url);
        }
        catch
        {
            return null;
        }
    }
}