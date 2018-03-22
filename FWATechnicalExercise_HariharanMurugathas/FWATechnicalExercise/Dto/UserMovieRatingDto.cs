using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FWATechnicalExercise.Dto
{
    public class UserMovieRatingDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        [Range(1,5)]
        public int Rating { get; set; }
    }
}
