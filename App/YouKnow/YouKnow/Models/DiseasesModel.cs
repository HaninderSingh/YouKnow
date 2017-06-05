using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouKnow.Models
{
    public class DiseasesModel
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Media { get; set; }
        public string Description { get; set; }
        public DateTime? FirstIdentifiedOn { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Contact { get; set; }
        public string KeyFacts { get; set; }
        public string Outbreaks { get; set; }
        public string Transmission { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string Prevention { get; set; }
        public string ConfirmedCases { get; set; }
        public double? Distance { get; set; }
    }
}
