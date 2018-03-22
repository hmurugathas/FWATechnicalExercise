using FWATechnicalExercise.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FWATechnicalExercise.Models
{
    public interface IRepository
    {
        Task<List<Movie>> GetAllMovies();

        Task<bool> SaveMovieRating(UserMovieRatingDto rating);

        Task<List<MovieDto>> GetFilteredMovies(int? year, string[] genres, string title);

        Task<List<MovieDto>> GetTop5MoviesByAverageRating();

        Task<List<MovieDto>> GetTop5MoviesByUserRating(int userId);
    }
}