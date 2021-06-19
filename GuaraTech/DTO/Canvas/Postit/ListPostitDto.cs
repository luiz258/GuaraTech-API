using GuaraTech.Euns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.Models
{
    public class ListPostitDto
    {
        public Guid Id { get; set; }
        public Guid IdCanvas { get; set; }
        public CanvasEnuns CanvasTypeBlock { get; set; }
        public String DescriptionPostit { get; set; }
        public String ColorPostit { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
