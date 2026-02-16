using System.Text.Json;
using PopChoice.Dtos;
using PopChoice.Models;

namespace PopChoice.Services;

public class MovieService : IMovieService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    public ILogger<MovieService> _logger { get; }

    public MovieService(IConfiguration configuration, ILogger<MovieService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _httpClient = new HttpClient();
    }

    

    public async Task<List<Movie>> SearchAsync(string query)
    {
        try { 
        var apiKey = _configuration["Omdb:ApiKey"];
        var url = $"https://www.omdbapi.com/?apikey={apiKey}&s={query}";

        var response = await _httpClient.GetStringAsync(url);

        var omdbResponse = JsonSerializer.Deserialize<OmdbSearchResponse>(
            response,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        if (omdbResponse?.Search == null)
            return new List<Movie>();

        return omdbResponse.Search
            .Take(2)
            .Select((m, index) => new Movie
            {
                Id = index + 1,
                Title = m.Title,
                Year = int.TryParse(m.Year, out var year) ? year : 0
            })
            .ToList();
        }
        catch (Exception ex) {
            _logger.LogError(ex, $"Error search movies for query {query}");
            return new List<Movie>();
        }
    }
}
