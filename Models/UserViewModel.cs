﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySchoolProj.Models
{
    public class UserViewModel
    {
       
        public string Id { get; set; }
            public string Email { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }

        public int age { get; set; }
            public string Role { get; set; }

    }
}