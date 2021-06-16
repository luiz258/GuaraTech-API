using GuaraTech.Euns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.DTO
{
    public class CreatedPostitCanvasDto
    {
        public Guid Id { get; set; }
        [Required]
        public Guid IdCanvas { get; set; }
        public int TypeBlockCanvas { get; set; }
        public String Description { get; set; }
        public String PostitColor { get; set; }
    }
}
