using Microsoft.EntityFrameworkCore;
using PopChoice.Data;
using PopChoice.Data.Entities;
using PopChoice.Services.Ai;
using PopChoice.Utilities;
using System.Text.Json;

namespace PopChoice.Services.Recommendations
{
    public class RecommendationService : IRecommendationService
    {
        private readonly AppDbContext _context;
        private readonly IAiEmbeddingService _embeddingService;
        private readonly ILogger<RecommendationService> _logger;

        public RecommendationService(
            AppDbContext context,
            IAiEmbeddingService embeddingService,
            ILogger<RecommendationService> logger)
        {
            _context = context;
            _embeddingService = embeddingService;
            _logger = logger;
        }
        public async Task<MovieEmbedding> GetBestMatchAsync(string preferenceText)
        {
            var userEmbedding = await _embeddingService.CreateEmbeddingAsync(preferenceText);

            var storedMovies = await _context.MovieEmbeddings.ToListAsync();

            var bestMatch = storedMovies
                .Select(m =>
                {
                    var vector = JsonSerializer.Deserialize<float[]>(m.EmbeddingJson)!;
                    var score = VectorMath.CosineSimilarity(userEmbedding, vector);

                    return new
                    {
                        Movie = m,
                        Score = score
                    };
                })
                .OrderByDescending(x => x.Score)
                .First();

            _logger.LogInformation(
                "[RECOMMENDATION] Winner={Title}, Score={Score}",
                bestMatch.Movie.Title,
                bestMatch.Score
            );

            return bestMatch.Movie;
        }
    }
}
