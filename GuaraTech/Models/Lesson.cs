﻿using GuaraTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Models
{
    public class Lesson : Entity
    {
        public Guid IdCourse { get; set; }
        public String Title { get; set; }
        public String Datails { get; set; }
        public String UrlVideo { get; set; }
        public Double Duration { get; set; }
        public int OrderVideo { get; set; }

    }
}
