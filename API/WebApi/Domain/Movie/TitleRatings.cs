using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.Movie
{
    public class TitleRatings
    {
        public string Id { get; set; }
        public float AverageRating { get; set; }
        public int NumVotes { get; set; }
    }
}
