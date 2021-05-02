using GuaraTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.DTO.Canvas.Postit
{
    public class ListCanvasByBlockDto
    {
   
        public ListCanvasByBlockDto()
        {
            Problem = new List<ListPostitDto>();
            Solution = new List<ListPostitDto>();
            KeyMetrics = new List<ListPostitDto>();
            UniqueValueProposition = new List<ListPostitDto>();
            UnfairAdvantage = new List<ListPostitDto>();
            Channels = new List<ListPostitDto>();
            CustomerSegments = new List<ListPostitDto>();
            Cost = new List<ListPostitDto>();
            Revenue = new List<ListPostitDto>();
        }


        public List<ListPostitDto> Problem { get; set; }
        public List<ListPostitDto> Solution { get; set; }
        public List<ListPostitDto> KeyMetrics { get; set; }
        public List<ListPostitDto> UniqueValueProposition { get; set; }
        public List<ListPostitDto> UnfairAdvantage { get; set; }
        public List<ListPostitDto> Channels { get; set; }
        public List<ListPostitDto> CustomerSegments { get; set; }
        public List<ListPostitDto> Cost { get; set; }
        public List<ListPostitDto> Revenue { get; set; }
    }
}
