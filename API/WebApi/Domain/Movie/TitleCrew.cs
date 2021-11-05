using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.Movie
{
    public class TitleCrew
    {
        public string Id { get; set; }
        public string Directors { get; set; }
        public string Writers { get; set; }
    }
}