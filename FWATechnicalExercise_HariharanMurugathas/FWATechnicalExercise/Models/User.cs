using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FWATechnicalExercise.Models
{
    public class User
    {
        public User()
        {
            UserMovieRatings = new HashSet<UserMovieRating>();
        }

        public int UserId { get; set; }

        public HashSet<UserMovieRating> UserMovieRatings { get; set; }
    }
}
