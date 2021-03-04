using GuaraTech.Euns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Models
{
    public class Canvas
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public String Title { get; set; }



    }
}
