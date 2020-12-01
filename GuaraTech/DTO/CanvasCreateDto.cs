using GuaraTech.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.DTO
{
    public class CanvasCreateDto:Entity
    {

        
        [Required(ErrorMessage = "O campo {0} é obrigatório !")]
        [StringLength(65, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public String Title { get; set; }

      }

    }
