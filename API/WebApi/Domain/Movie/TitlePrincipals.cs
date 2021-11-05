using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.Movie
{
    public class TitlePrincipals
    {
        public string Id { get; set; }
        public int Ordering { get; set; }
        public string NameId { get; set; }
        public string Category { get; set; }
        public string Job { get; set; }
        public string Characters { get; set; }
    }
}