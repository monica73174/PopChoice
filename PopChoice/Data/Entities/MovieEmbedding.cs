namespace PopChoice.Data.Entities
{
    public class MovieEmbedding
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string EmbeddingJson { get; set; } = string.Empty;
    }
}
