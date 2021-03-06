using GuaraTech.Euns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Models
{
    public class CanvasPostit
    {
        public Guid Id { get; set; }
        public Guid IdCanvas { get; set; }
        public CanvasEnuns TypeBlockCanvas { get; set; }
        public String Description { get; set; }
        public String PostitColor { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
