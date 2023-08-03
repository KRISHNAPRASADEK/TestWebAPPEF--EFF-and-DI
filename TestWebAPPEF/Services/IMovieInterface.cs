using Microsoft.AspNetCore.Mvc;
using TestWebAPPEF.Models;

namespace TestWebAPPEF.Services
{
    public interface IMovieInterface
    {
        Task<IEnumerable<MovieDto>> GetMovie();
        Task<Movie> PostMovie(MovieDto movie);
        Task<Movie> DeleteMovie(int id);
        Task<Movie> PutMovie(int id, MovieDto movie);
    }
}
