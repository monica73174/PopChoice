using Microsoft.AspNetCore.Mvc;
using PopChoice.Data;
using PopChoice.Models;
using PopChoice.Services;
using PopChoice.Services.Ai;
using PopChoice.Services.Recommendations;
using PopChoice.ViewModels;
using System.Diagnostics;


namespace PopChoice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
           private readonly IMovieService movieService;

        public IAiEmbeddingService EmbeddingService { get; }
        private readonly IRecommendationService _recommendationService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService, IAiEmbeddingService embeddingService,AppDbContext dbContext, IRecommendationService recommendationService)
        {
            _logger = logger;
            this.movieService = movieService;
            EmbeddingService = embeddingService;
            _recommendationService = recommendationService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Questions");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Questions()
        {
            return View(new QuestionsViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Questions(QuestionsViewModel model)
        {
            var preferenceText = BuildPreferenceText(model);

            var winner = await _recommendationService.GetBestMatchAsync(preferenceText);

            return View("Winner", winner);
        }

        private string BuildPreferenceText(QuestionsViewModel model)
        {
            return $"""
            Favorite movie: {model.FavoriteMovie}.
            Era preference: {model.EraPreference}.
            Mood: {model.Mood}.
            """;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
