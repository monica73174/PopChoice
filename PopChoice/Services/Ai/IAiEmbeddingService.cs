namespace PopChoice.Services.Ai
{
    public interface IAiEmbeddingService
    {
        Task<float[]> CreateEmbeddingAsync(string text);
    }
}
