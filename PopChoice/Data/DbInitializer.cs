using System.Text.Json;
using PopChoice.Data.Entities;
using PopChoice.Services.Ai;

namespace PopChoice.Data;

public static class DbInitializer
{
    public static async Task SeedEmbeddingsAsync(
        AppDbContext context,
        IAiEmbeddingService embeddingService)
    {
        // DEV MODE: wipe and reseed
        context.MovieEmbeddings.RemoveRange(context.MovieEmbeddings);
        await context.SaveChangesAsync();

        var movies = new[]
        {
            new
            {
                Title = "The Shawshank Redemption",
                Description = "A hopeful prison drama about redemption and friendship between two inmates."
            },
            new
            {
                Title = "The Dark Knight",
                Description = "A gritty superhero crime thriller exploring chaos, justice, and moral conflict."
            },
            new
            {
                Title = "La La Land",
                Description = "A romantic musical about ambition, love, and dreams in Los Angeles."
            },
            new
            {
                Title = "Superbad",
                Description = "A crude but heartfelt coming-of-age comedy about teenage friendship and growing up."
            },
            new
            {
                Title = "The Matrix",
                Description = "A science fiction action film about reality, artificial intelligence, and rebellion against control."
            }
        };

        foreach (var movie in movies)
        {
            var vector = await embeddingService.CreateEmbeddingAsync(movie.Description);

            context.MovieEmbeddings.Add(new MovieEmbedding
            {
                Title = movie.Title,
                Description = movie.Description,
                EmbeddingJson = JsonSerializer.Serialize(vector)
            });
        }

        await context.SaveChangesAsync();
    }
}