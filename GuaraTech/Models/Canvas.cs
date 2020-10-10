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
        [Required(ErrorMessage ="O campo {0} é obrigatório !")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório !")]
        public Guid IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório !")]
        public String Title { get; set; }

        public CanvasBlock CanvasBlock { get; set; }

    }
}
