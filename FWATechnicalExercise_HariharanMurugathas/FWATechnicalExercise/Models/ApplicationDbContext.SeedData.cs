using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FWATechnicalExercise.Models
{
    public static class ApplicationDbContextSeedData
    {
        static object synchlock = new object();
        static volatile bool seeded = false;

        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            if (!seeded && context.Movies.Count() == 0)
            {
                lock (synchlock)
                {
                    if (!seeded)
                    {
                        var movies = GenerateMovies();
                        context.Movies.AddRange(movies);

                        var users = GenerateUsers();
                        context.Users.AddRange(users);

                        var ratings = GenerateUserMovieRating();
                        context.UserMovieRatings.AddRange(ratings);

                        context.SaveChanges();

                        seeded = true;
                    }
                }
            }
        }

        public static Movie[] GenerateMovies()
        {
            return new Movie[] {
                new Movie {  MovieId = 1, Title="Tomb Raider", YearOfRelease=2018, Genre="Action", RunningTime=120},
                new Movie {  MovieId = 2, Title="Black Panther", YearOfRelease=2018, Genre="Action", RunningTime=120},
                new Movie {  MovieId = 3, Title="The greatest showman", YearOfRelease=2017, Genre="Drama", RunningTime=90},
                new Movie {  MovieId = 4, Title="The shape of water", YearOfRelease=2017, Genre="Drama", RunningTime=130},
                new Movie {  MovieId = 5, Title="Arrival", YearOfRelease=2016, Genre="Mystery", RunningTime=180},
                new Movie {  MovieId = 6, Title="La La Land", YearOfRelease=2016, Genre="Romance", RunningTime=70 },
                new Movie {  MovieId = 7, Title="Thor", YearOfRelease=2011, Genre="Fantasy", RunningTime=110 }
            };
        }

        public static User[] GenerateUsers()
        {
            return new User[] {
                new User{  UserId = 1},
                new User {  UserId = 2},
                new User {  UserId = 3}
            };
        }

        public static UserMovieRating[] GenerateUserMovieRating()
        {
            return new UserMovieRating[] {
                new UserMovieRating{  UserId = 1, MovieId = 1, Rating=5},
                new UserMovieRating {  UserId = 2, MovieId = 1, Rating=2},
                new UserMovieRating {  UserId = 3, MovieId = 1, Rating=3},

                new UserMovieRating{  UserId = 1, MovieId = 2, Rating=5},
                new UserMovieRating {  UserId = 2, MovieId = 2, Rating=2},
                new UserMovieRating {  UserId = 3, MovieId = 2, Rating=3}
            };
        }
    }
}
