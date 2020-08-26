using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Models
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String UrlVideo { get; set; }
        public Double Duration { get; set; }

    }
}
