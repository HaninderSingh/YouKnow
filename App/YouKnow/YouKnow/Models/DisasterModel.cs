using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouKnow.Models
{
    public class DisasterModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Media { get; set; }
        public string Description { get; set; }
        public string DisasterTypeName { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Contact { get; set; }
        public double? Distance { get; set; }
    }
}
