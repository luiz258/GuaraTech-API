﻿using GuaraTech.Euns;
using GuaraTech.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.DTO
{
    public class CourseDto
    {
    
        public Guid Id { get; set; }

        [Required(ErrorMessage ="O campo {0} é obrigatorio")]
        public String Title { get; set; }

        public String Details { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public StateCourseEnuns StateCourse { get; set; }
    }
}
