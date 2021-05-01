using GuaraTech.Euns;
using GuaraTech.Models.Euns;
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

        public String DescriptionCanvas { get; set; }

        public ECanvasState CanvasState { get; set; }

        public bool IsPrivate { get; set; }
        public DateTime DateCreated { get; set; }



    }
}
