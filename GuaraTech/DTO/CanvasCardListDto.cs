using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.DTO
{
    public class CanvasCardListDto
    {
       
        public Guid ID { get; set; }
        public String Title { get; set; }
        public String FullName { get; set; }
        public bool isPrivate { get; set; }
        public int completedPercent{ get; set; }
    }
}
