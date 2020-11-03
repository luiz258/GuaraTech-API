using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.DTO
{
    public class TeamCanvasDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório !")]
        public Guid IdCanvas { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório !")]
        public Guid IdUser { get; set; }

    }
}
