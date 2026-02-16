using PopChoice.Data.Entities;

namespace PopChoice.Services.Recommendations
{
    public interface IRecommendationService
    {
        Task<MovieEmbedding> GetBestMatchAsync(string preferenceText);
    }
}
