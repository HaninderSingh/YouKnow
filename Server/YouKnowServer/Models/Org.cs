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
    
    public partial class Org
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Org()
        {
            this.OrgUsers = new HashSet<OrgUser>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid OrgTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.Guid> CountryId { get; set; }
        public Nullable<System.Guid> StateId { get; set; }
        public Nullable<System.Guid> CityId { get; set; }
        public Nullable<System.Guid> AreaId { get; set; }
        public int Jurisidiction { get; set; }
    
        public virtual OrgType OrgType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrgUser> OrgUsers { get; set; }
    }
}