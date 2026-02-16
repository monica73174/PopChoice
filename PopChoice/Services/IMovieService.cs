using PopChoice.Models;

namespace PopChoice.Services
{
    public interface IMovieService
    {
          Task<List<Movie>> SearchAsync(string query);
    }
}
