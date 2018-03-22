using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FWATechnicalExercise.Dto
{
    public class MovieDto
    {
        public string Title { get; set; }

        public int YearOfRelease { get; set; }

        public int RunningTime { get; set; }

        public double AverageRating { get; set; }
    }
}
