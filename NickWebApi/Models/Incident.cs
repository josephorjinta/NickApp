using System;
using System.Collections.Generic;

#nullable disable

namespace NickWebApi.Models
{
    public partial class Incident
    {
        public string IncidentCode { get; set; }
        public string IncidentName { get; set; }
        public DateTime? IncidentDate { get; set; }
        public string IncidentLocation { get; set; }
        public string IncidentRecordBy { get; set; }
        public bool? IncidentAddressed { get; set; }
    }
}
