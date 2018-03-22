using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FWATechnicalExercise.Dto;
using FWATechnicalExercise.Models;
using Microsoft.AspNetCore.Mvc;

namespace FWATechnicalExercise.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private IRepository _repository;

        public MoviesController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]        
        public async Task<IActionResult> Search(int? year, string genres, string title)
        {
            title = title?.Trim();
            genres = genres?.Trim();

            if (!year.HasValue && string.IsNullOrEmpty(genres) && string.IsNullOrEmpty(title))
            {
                return BadRequest();
            }

            string[] genresList = { };

            if (!string.IsNullOrEmpty(genres))
            {
                genresList = genres.ToLower().Split(",");
            }

            var movies = await _repository.GetFilteredMovies(year, genresList, title);

            if (movies.Count == 0)
            {
                return NotFound();
            }

            return Ok(movies);
        }

        [HttpGet("Top5Movies")]
        public async Task<IActionResult> Top5Movies()
        {
            var movies = await _repository.GetTop5MoviesByAverageRating();

            if (movies.Count == 0)
            {
                return NotFound();
            }

            return Ok(movies);
        }

        [HttpGet("Top5Movies/{id:int:min(1)}")]
        public async Task<IActionResult> Top5Movies(int id)
        {
            var movies = await _repository.GetTop5MoviesByUserRating(id);

            if (movies.Count == 0)
            {
                return NotFound();
            }

            return Ok(movies);
        }

        [HttpPost()]
        public async Task<IActionResult> AddRating([FromBody]UserMovieRatingDto rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var success = await _repository.SaveMovieRating(rating);

            if (success)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
