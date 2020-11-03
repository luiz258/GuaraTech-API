﻿using GuaraTech.Euns;
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
