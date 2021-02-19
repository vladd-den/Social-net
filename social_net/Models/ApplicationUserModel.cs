﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_net.Models
{
    public class ApplicationUserModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
