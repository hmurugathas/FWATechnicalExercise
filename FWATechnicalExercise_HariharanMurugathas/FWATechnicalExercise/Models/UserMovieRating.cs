using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FWATechnicalExercise.Models
{
    public class UserMovieRating
    {
        [Key]
        [Column(Order = 1)]
        public int UserId { get; set; }

        public User User { get; set; }

        [Key]
        [Column(Order = 2)]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
