using GuaraTech.Euns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuaraTech.DTO
{
    public class CanvasGetBlock
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório !")]
        public Guid Id { get; set; }

        public Guid IdUser { get; set; }

        public string Title { get; set; }

        public String ValueOffer { get; set; }

        public String CustomerSegment { get; set; }

        public String CommunicationChannels { get; set; }

        public String CustomerRelationship { get; set; }

        public String KeyFeatures { get; set; }

        public String MainActivities { get; set; }

        public String Description { get; set; }

        public String Partnerships { get; set; }

        public String Recipe { get; set; }

        public String Cost { get; set; }


    }
}
