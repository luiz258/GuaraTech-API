using GuaraTech.Euns;
using GuaraTech.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Models
{
    public class Course : Entity
    {
        public String Title { get; set; }
        public String Details { get; set; }
        public String Tag { get; set; } // mudar tipo
        public StateCourseEnuns StateCourse { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }

    }
}
