using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FWATechnicalExercise.Models
{    
    public class Movie
    {
        public Movie()
        {
            UserMovieRatings = new HashSet<UserMovieRating>();
        }

        public int MovieId { get; set; }
 
        public string Title { get; set; }
 
        public int YearOfRelease { get; set; }

        public string Genre { get; set; }
 
        public int RunningTime { get; set; }
 
        public HashSet<UserMovieRating> UserMovieRatings { get; set; }
    }
}
