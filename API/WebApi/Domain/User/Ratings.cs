using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.User
{
    public class Ratings
    {
        public string Username { get; set; }
        public string TitleId { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
    }
}
