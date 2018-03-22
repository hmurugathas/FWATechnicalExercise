using FWATechnicalExercise.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FWATechnicalExercise.Models
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }


        public async Task<List<Movie>> GetAllMovies()
        {
            return await (from m in _context.Movies select m).ToListAsync();
        }

        public async Task<List<MovieDto>> GetFilteredMovies(int? year, string[] genres, string title)
        {
            var query = from m in _context.Movies
                        select m;

            if (year.HasValue)
            {
                query = from m in query
                        where m.YearOfRelease == year.Value
                        select m;
            }

            if (genres != null && genres.Length > 0)
            {
                query = from m in query
                        where genres.Contains(m.Genre.ToLower())
                        select m;
            }

            if (!string.IsNullOrEmpty(title))
            {
                query = from m in query
                        where m.Title.ToLower().Contains(title.ToLower())
                        select m;
            }

            var results = from m in query
                          join r in _context.UserMovieRatings
                              on m.MovieId equals r.MovieId into mr
                          from r in mr.DefaultIfEmpty()
                          group r by m into g
                          select new MovieDto
                          {
                              Title = g.Key.Title,
                              RunningTime = g.Key.RunningTime,
                              YearOfRelease = g.Key.YearOfRelease,
                              AverageRating = Math.Round(g.Average(r => r == null ? 0 : r.Rating) * 2, MidpointRounding.AwayFromZero) / 2
                          };

            return await results.ToListAsync();
        }

        public async Task<List<MovieDto>> GetTop5MoviesByAverageRating()
        {
            var query = from m in _context.Movies
                        join r in _context.UserMovieRatings
                        on m.MovieId equals r.MovieId into mr
                        from r in mr.DefaultIfEmpty()
                        group r by m into g
                        orderby g.Average(r => r == null ? 0 : r.Rating) descending, g.Key.Title
                        select new MovieDto
                        {
                            Title = g.Key.Title,
                            RunningTime = g.Key.RunningTime,
                            YearOfRelease = g.Key.YearOfRelease,
                            AverageRating = Math.Round(g.Average(r => r == null ? 0 : r.Rating) * 2, MidpointRounding.AwayFromZero) / 2
                        };
            
            return await query.Take(5).ToListAsync();
        }

        public async Task<List<MovieDto>> GetTop5MoviesByUserRating(int userId)
        {
            var query = from u in _context.Users
                        join ur in _context.UserMovieRatings
                        on u.UserId equals ur.UserId
                        join m in _context.Movies
                        on ur.MovieId equals m.MovieId
                        where u.UserId == userId
                        orderby ur.Rating descending, m.Title
                        select new MovieDto
                        {
                            Title = m.Title,
                            RunningTime = m.RunningTime,
                            YearOfRelease = m.YearOfRelease,
                            AverageRating = ur.Rating
                        };

            return await query.Take(5).ToListAsync();
        }

        public async Task<bool> SaveMovieRating(UserMovieRatingDto rating)
        {
            var movieExists = await _context.Movies.AnyAsync(m => m.MovieId == rating.MovieId);

            if (!movieExists)
            {
                return false;
            }

            var userExists = await _context.Users.AnyAsync(u => u.UserId == rating.UserId);

            if (!userExists)
            {
                return false;
            }

            var userMovieRating = await (from r in _context.UserMovieRatings
                                   where r.MovieId == rating.MovieId && r.UserId == rating.UserId
                                   select r).FirstOrDefaultAsync();

            if (userMovieRating == null)
            {
                _context.UserMovieRatings.Add(new UserMovieRating { MovieId = rating.MovieId, UserId = rating.UserId, Rating = rating.Rating });
            }
            else
            {
                userMovieRating.Rating = rating.Rating;
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
