﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YouKnowServer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Disease
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Disease()
        {
            this.DiseaseTags = new HashSet<DiseaseTag>();
            this.TreatingHospitals = new HashSet<TreatingHospital>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Media { get; set; }
        public string Description { get; set; }
        public Nullable<System.Guid> OrgUserId { get; set; }
        public Nullable<System.DateTime> FirstIdentifiedOn { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public System.Data.Entity.Spatial.DbGeography GroundZero { get; set; }
        public string Contact { get; set; }
        public string KeyFacts { get; set; }
        public string Outbreaks { get; set; }
        public string Transmission { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string Prevention { get; set; }
        public string ConfirmedCases { get; set; }
    
        public virtual OrgUser OrgUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiseaseTag> DiseaseTags { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TreatingHospital> TreatingHospitals { get; set; }
    }
}
