using GuaraTech.Euns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.DTO
{
    public class CanvasEditDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório !")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório !")]
        public Guid IdUser { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório !")]
        [StringLength(600, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public String Description { get; set; }


    }
}
