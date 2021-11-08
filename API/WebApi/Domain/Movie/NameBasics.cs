using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.Movie
{
    public class NameBasics
    {
        public string Id { get; set; }
        public string PrimaryName { get; set; }
        public string BirthYear { get; set; }
        public string DeathYear { get; set; }
        public string PrimaryProfession { get; set; }
        public string KnownForTitles {get; set;}
    }
}