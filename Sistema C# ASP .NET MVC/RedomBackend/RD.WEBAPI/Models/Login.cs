﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.WEBAPI.Models
{
    public class Login
    {
        public int LoginID { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
