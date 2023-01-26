using System;
using System.Collections.Generic;
using SQLite;


namespace NickApp.Models
{
    public partial class Incident
    {
        [PrimaryKey]

        public string IncidentCode { get; set; }
        public string IncidentName { get; set; }
        public DateTime? IncidentDate { get; set; }
        public string IncidentLocation { get; set; }
        public string IncidentRecordBy { get; set; }
        public bool? IncidentAddressed { get; set; }
    }
}
