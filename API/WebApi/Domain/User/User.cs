﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.User
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsAdult { get; set; }
    }
}
