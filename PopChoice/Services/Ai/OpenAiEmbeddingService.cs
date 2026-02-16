
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.IO;

namespace PopChoice.Services.Ai
{
    public class OpenAiEmbeddingService : IAiEmbeddingService
    {
        public IConfiguration Configuration { get; }
        public ILogger<OpenAiEmbeddingService> Logger { get; }
        public HttpClient httpClient { get; }


        public OpenAiEmbeddingService(IConfiguration configuration, ILogger<OpenAiEmbeddingService> logger)
        {
            Configuration = configuration;
            Logger = logger;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.openai.com/v1/")
            };

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    Configuration["OpenAI:ApiKey"]
                );

        }



        public async Task<float[]> CreateEmbeddingAsync(string text)
        {
            var request = new
            {
                model = Configuration["OpenAI:EmbeddingModel"],
                input = text
            };

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            var response = await httpClient.PostAsync("embeddings", content);

            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError(
                    "OpenAI embedding request failed with status {Status}",
                    response.StatusCode
                );
                return Array.Empty<float>();
            }


            using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);

            var vector = doc
                .RootElement
                .GetProperty("data")[0]
                .GetProperty("embedding")
                .EnumerateArray()
                .Select(x => x.GetSingle())
                .ToArray();

            Logger.LogInformation(
                    "Generated embedding with length {Length}",
                    vector.Length
                );

            return vector;
        }
    }
}
