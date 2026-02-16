using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PopChoice.Data;
using PopChoice.Data.Entities;
using PopChoice.Services.Ai;
using PopChoice.Services.Recommendations;
using Xunit;

public class RecommendationServiceTests
{
    public ILogger<RecommendationServiceTests> Logger { get; }


    private AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetBestMatchAsync_ReturnsHighestSimilarityMovie()
    {
        // Arrange
        var dbContext = CreateDbContext();

        var movie1 = new MovieEmbedding
        {
            Title = "Movie One",
            Description = "Test A",
            EmbeddingJson = System.Text.Json.JsonSerializer.Serialize(new float[] { 1, 0 })
        };

        var movie2 = new MovieEmbedding
        {
            Title = "Movie Two",
            Description = "Test B",
            EmbeddingJson = System.Text.Json.JsonSerializer.Serialize(new float[] { 0, 1 })
        };

        dbContext.MovieEmbeddings.AddRange(movie1, movie2);
        await dbContext.SaveChangesAsync();

        var embeddingServiceMock = new Mock<IAiEmbeddingService>();
        embeddingServiceMock
            .Setup(x => x.CreateEmbeddingAsync(It.IsAny<string>()))
            .ReturnsAsync(new float[] { 1, 0 });

        var loggerMock = new Mock<ILogger<RecommendationService>>();

        var service = new RecommendationService(
            dbContext,
            embeddingServiceMock.Object,
            loggerMock.Object);

        // Act
        var result = await service.GetBestMatchAsync("anything");

        // Assert
        result.Title.Should().Be("Movie One");
    }
}